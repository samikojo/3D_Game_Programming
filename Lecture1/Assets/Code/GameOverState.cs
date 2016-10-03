using UnityEngine.SceneManagement;

namespace GameProgramming3D.State
{
	public class GameOverState : StateBase
	{
		public GameOverState()
		{
			State = StateType.GameOverState;
			AddTransition( TransitionType.GameOverToGame, StateType.GameState );
			AddTransition( TransitionType.GameOverToMenu, StateType.MenuState );
		}

		public override void StateActivated()
		{
			SceneManager.LoadScene( 2 );
		}
	}
}
