using UnityEngine;
using System.Collections;
using GameProgramming3D.State;

namespace GameProgramming3D.GUI
{
	public class GameOverGUI : MonoBehaviour
	{
		public void OnRestartGamePressed()
		{
			GameManager.Instance.StateManager.PerformTransition( TransitionType.GameOverToGame );
		}

		public void OnToMainMenuPressed()
		{
			GameManager.Instance.StateManager.PerformTransition( TransitionType.GameOverToMenu );
		}
	}
}
