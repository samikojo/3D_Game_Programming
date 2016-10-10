using System.Collections.Generic;

namespace GameProgramming3D.State
{
	public enum StateType
	{
		Error = -1,
		MenuState,
		GameState,
		GameOverState
	}

	public enum TransitionType
	{
		Error = -1,
		MenuToGame,
		GameToMenu,
		GameToGameOver,
		GameOverToGame,
		GameOverToMenu
	}

	public class GameStateManager
	{
		#region Delegates and events
		public delegate void GameStateChangedDelegate( StateType type );

		public event GameStateChangedDelegate GameStateChanged;
		#endregion Delegates and events

		private List<StateBase> _states = new List< StateBase >(); 

		public StateBase CurrentState { get; private set; }

		public StateType CurrentStateType
		{
			get { return CurrentState.State; }
		}

		public GameStateManager( StateBase initialState )
		{
			if ( AddState( initialState ) )
			{
				CurrentState = initialState;
				CurrentState.StateActivated();
			}
		}

		public bool AddState( StateBase state )
		{
			bool exists = false;
			foreach ( var s in _states )
			{
				if ( s.State == state.State )
				{
					exists = true;
				}
			}

			if ( !exists )
			{
				_states.Add( state );
			}

			return !exists;
		}

		public bool RemoveState( StateType stateType )
		{
			StateBase state = null;
			foreach ( var s in _states )
			{
				if ( s.State == stateType )
				{
					state = s;
				}
			}

			return state != null && _states.Remove( state );
		}

		public void PerformTransition( TransitionType transition )
		{
			if ( transition == TransitionType.Error )
			{
				return;
			}

			var targetStateType = CurrentState.GetTargetStateType( transition );
			if ( targetStateType == StateType.Error || targetStateType == CurrentStateType )
			{
				return;
			}

			foreach ( var stateBase in _states )
			{
				if ( stateBase.State == targetStateType )
				{
					CurrentState.StateDeactivating();
					CurrentState = stateBase;
					CurrentState.StateActivated();
				}
			}
		}

		public void RaiseGameStateChangedEvent( StateType type )
		{
			if ( GameStateChanged != null )
			{
				GameStateChanged( type );
			}
		}
	}
}
