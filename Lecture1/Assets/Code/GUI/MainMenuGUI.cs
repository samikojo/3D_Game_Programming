using UnityEngine;
using UnityEngine.UI;
using GameProgramming3D.SaveLoad;

namespace GameProgramming3D.GUI
{
	public class MainMenuGUI : MonoBehaviour
	{
		[SerializeField] private Button _loadGameButton;

		protected void Awake()
		{
			_loadGameButton.interactable = SaveSystem.DoesSaveExist();
		}

		public void OnNewGamePressed()
		{
			GameManager.Instance.StartGame ();
		}

		public void OnLoadGamePressed()
		{
			GameManager.Instance.LoadGame ();
		}

		public void OnQuitGamePressed()
		{
			Application.Quit();
		}
	}
}
