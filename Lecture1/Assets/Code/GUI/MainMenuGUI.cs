using UnityEngine;
using GameProgramming3D.State;

namespace GameProgramming3D.GUI
{
	public class MainMenuGUI : MonoBehaviour
	{
		public void OnNewGamePressed()
		{
			GameManager.Instance.StateManager.PerformTransition( TransitionType.MenuToGame );
		}

		public void OnQuitGamePressed()
		{
			Application.Quit();
		}
	}
}
