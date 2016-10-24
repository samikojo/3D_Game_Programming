using UnityEngine;
using GameProgramming3D.State;

namespace GameProgramming3D.GUI
{
	public class GameOverGUI : SceneGUI
	{
		public void OnRestartGamePressed()
		{
			GameManager.Instance.StartGame (TransitionType.GameOverToGame);
		}

		public void OnToMainMenuPressed()
		{
			GameManager.Instance.StateManager.PerformTransition( TransitionType.GameOverToMenu );
		}
	}
}
