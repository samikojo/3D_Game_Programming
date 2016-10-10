using System;
using System.Collections.Generic;
using GameProgramming3D.SaveLoad;
using GameProgramming3D.State;
using UnityEngine;

namespace GameProgramming3D
{
	public class GameManager : MonoBehaviour
	{
		#region Statics
		private static GameManager _instance;

		public static GameManager Instance
		{
			get
			{
				if ( _instance == null )
				{
					var go = new GameObject("GameManager");
					_instance = go.AddComponent< GameManager >();
					_instance.Init();
				}
				return _instance;
			}
		}
		#endregion

		public event System.Action<Player> TurnChanged;
		public event System.Action< int > LevelLoaded;

		private int _playerIndex = -1;
		private InputManager _inputManager;

		[Tooltip( "How much time player has to complete their turn (in seconds)." )]
		[SerializeField] private float _turnTime; // In seconds.

		private CountdownTimer _turnTimer;

		public CountdownTimer TurnTimer
		{
			get
			{
				if ( _turnTimer == null )
				{
					_turnTimer = gameObject.GetOrAddComponent< CountdownTimer >();
					_turnTimer.SetTime( _turnTime );
					_turnTimer.TimerFinished += HandleTimerFinished;
				}
				return _turnTimer;
			}
		}

		public void PlayerLost ()
		{
			EndGame();
		}

		private void EndGame ()
		{
			TurnTimer.Stop();
			StateManager.PerformTransition( TransitionType.GameToGameOver );
		}

		public GameStateManager StateManager { get; private set; }

		private void HandleTimerFinished()
		{
			ChangeTurn ();
		}

		private void Init()
		{
			DontDestroyOnLoad( gameObject );
			InitGameStateManager ();
			InitInputManager ();
		}

		private void InitGameStateManager ()
		{
			StateManager = new GameStateManager( new MenuState() );
			StateManager.AddState( new GameState() );
			StateManager.AddState( new GameOverState() );
		}

		private void InitInputManager()
		{
			_inputManager = FindObjectOfType<InputManager> ();
			if(_inputManager == null)
			{
				_inputManager = gameObject.GetOrAddComponent<InputManager> ();
			}
			_inputManager.Init ();
		}

		private Unit _selectedUnit;

		public Unit SelectedUnit
		{
			get { return _selectedUnit; }
			set {
				SelectUnit( value );
			}
		}

		public Player ActivePlayer
		{
			get
			{
				return _playerIndex >= 0 && _playerIndex < AllPlayers.Length 
					? AllPlayers[ _playerIndex ] 
					: null;
			}
		}

		private void SelectUnit( Unit value )
		{
			if ( value == null || value.AssociatedPlayer == ActivePlayer )
			{
				if ( _selectedUnit != null )
				{
					_selectedUnit.Select( false );
				}
				_selectedUnit = value;
				if ( _selectedUnit != null )
				{
					_selectedUnit.Select( true );
				}
			}
		}

		private GUIManager _guiManager;

		public GUIManager GUIManager
		{
			get
			{
				if ( _guiManager == null )
				{
					_guiManager = FindObjectOfType< GUIManager >();
					if ( _guiManager == null )
					{
						var guiManagerGo = new GameObject("GUIManager");
						_guiManager = guiManagerGo.AddComponent< GUIManager >();
					}
				}
				return _guiManager;
			}
		}

		public Unit[] AllUnits { get; private set; }
		public Player[] AllPlayers { get; private set; }

		protected void Awake()
		{
			if ( _instance == null )
			{
				_instance = this;
				Init ();
			}
			else if ( _instance != this )
			{
				Destroy ( this );
			}
		}

		protected void OnLevelWasLoaded(int index)
		{
			OnLevelLoaded( index );
		}

		protected void OnDisable()
		{
			TurnTimer.TimerFinished -= HandleTimerFinished;
		}

		protected void OnLevelLoaded( int index )
		{
			if ( LevelLoaded != null )
			{
				LevelLoaded( index );
			}
		}

		public void StartGame()
		{
			StateManager.GameStateChanged += GameStarted;
			StateManager.PerformTransition ( TransitionType.MenuToGame );
		}

		public void LoadGame()
		{
			StateManager.GameStateChanged += GameLoaded;
			StateManager.PerformTransition ( TransitionType.MenuToGame );
		}

		private void GameStarted( StateType state )
		{
			StateManager.GameStateChanged -= GameStarted;
			InitPlayers ();
			ChangeTurn ();
		}

		private void GameLoaded(StateType state)
		{
			StateManager.GameStateChanged -= GameLoaded;

			InitPlayers ();
			LoadGameData ();
			SelectedUnit = ActivePlayer.UnitInTurn;
			TurnTimer.StartTimer ();
		}

		private void LoadGameData()
		{
			GameData data = SaveSystem.Load<GameData> ();
			Player activePlayer = GetPlayerById ( data.ActivePlayer );
			_playerIndex = Array.IndexOf ( AllPlayers, activePlayer );

			TurnTimer.CurrentTime = data.CurrentTime;

			foreach ( Player player in AllPlayers )
			{
				player.SetActiveUnit ( data.Turns[player.Id] );
			}

			foreach(UnitData unitData in data.UnitDatas)
			{
				Player player = GetPlayerById ( unitData.PlayerId );
				Unit unit = player.GetUnitById ( unitData.Id );
				unit.SetUnit ( unitData );
			}
		}

		private Player GetPlayerById(int id)
		{
			Player player = null;
			foreach (Player p in AllPlayers)
			{
				if(p.Id == id)
				{
					player = p;
				}
			}

			return player;
		}

		private void InitPlayers ()
		{
			AllPlayers = FindObjectsOfType<Player> ();
			for ( int i = 0; i < AllPlayers.Length; ++i )
			{
				AllPlayers[i].Init ();
			}

			if ( !CheckPlayerIds () )
			{
				Debug.LogError ( "GameManager: Players doesn't have unique ids!" );
				Debug.Break ();
			}

			AllUnits = FindObjectsOfType<Unit> ();
		}

		public void ChangeTurn()
		{
			_playerIndex++;
			if(_playerIndex >= AllPlayers.Length)
			{
				_playerIndex = 0;
			}
			ActivePlayer.StartTurn ();
			TurnTimer.Stop ();
			TurnTimer.Reset ();
			TurnTimer.StartTimer ();
			OnTurnChanged ();
		}

		public void SaveGame()
		{
			GameData data = new GameData();
			data.ActivePlayer = ActivePlayer.Id;
			data.CurrentTime = TurnTimer.CurrentTime;

			foreach ( var player in AllPlayers )
			{
				int unitId = -1;
				if ( player.UnitInTurn != null )
				{
					unitId = player.UnitInTurn.Id;
				}

				data.Turns.Add( player.Id, unitId );
			}

			foreach ( var unit in AllUnits )
			{
				data.UnitDatas.Add( new UnitData( unit ) );
			}

			SaveSystem.Save( data );
		}

		protected void OnTurnChanged()
		{
			if(TurnChanged != null)
			{
				TurnChanged ( ActivePlayer );
			}
		}

		private bool CheckPlayerIds ()
		{
			HashSet<int> playerIds = new HashSet<int> ();
			foreach ( Player player in AllPlayers )
			{
				if ( !playerIds.Add ( player.Id ) )
				{
					return false;
				}
			}

			return true;
		}
	}
}
