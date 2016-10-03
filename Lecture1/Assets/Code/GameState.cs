using System;
using UnityEngine.SceneManagement;

namespace GameProgramming3D.State
{
	public class GameState : StateBase
	{
		public GameState()
		{
			State = StateType.GameState;
			AddTransition( TransitionType.GameToGameOver, StateType.GameOverState );
			AddTransition( TransitionType.GameToMenu, StateType.MenuState );
		}

		public override void StateActivated()
		{
			GameManager.Instance.LevelLoaded += HandleLevelLoaded;
			SceneManager.LoadScene( 1 );
		}

		private void HandleLevelLoaded ( int index )
		{
			GameManager.Instance.LevelLoaded -= HandleLevelLoaded;
			GameManager.Instance.StartGame();
		}
	}
}
