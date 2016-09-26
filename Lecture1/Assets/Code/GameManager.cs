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

		private void HandleTimerFinished()
		{
			ChangeTurn ();
		}

		private void Init()
		{
			AllUnits = FindObjectsOfType<Unit> ();
			AllPlayers = FindObjectsOfType< Player >();
			for ( int i = 0; i < AllPlayers.Length; ++i )
			{
				AllPlayers[i].Init();
			}

			InitInputManager ();
			StartGame ();
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
				Destroy( this );
			}
		}

		protected void OnDisable()
		{
			TurnTimer.TimerFinished -= HandleTimerFinished;
		}

		public void StartGame()
		{
			ChangeTurn ();
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

		protected void OnTurnChanged()
		{
			if(TurnChanged != null)
			{
				TurnChanged ( ActivePlayer );
			}
		}
	}
}
