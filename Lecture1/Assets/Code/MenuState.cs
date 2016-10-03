using UnityEngine.SceneManagement;

namespace GameProgramming3D.State
{
	public class MenuState : StateBase
	{
		public MenuState()
		{
			State = StateType.MenuState; // The type of this state.
			AddTransition( TransitionType.MenuToGame, StateType.GameState );
		}

		// Called when state is activated.
		public override void StateActivated()
		{
			SceneManager.LoadScene( 0 );
		}
	}
}
