using UnityEngine;
using UnityEngine.UI;
using GameProgramming3D.SaveLoad;
using GameProgramming3D.State;

namespace GameProgramming3D.GUI
{
	public class MainMenuGUI : SceneGUI
	{
		[SerializeField] private Button _loadGameButton;

		protected void Awake()
		{
			_loadGameButton.interactable = SaveSystem.DoesSaveExist();
		}

		public void OnNewGamePressed()
		{
			GameManager.Instance.StartGame (TransitionType.MenuToGame);
		}

		public void OnLoadGamePressed()
		{
			GameManager.Instance.LoadGame ();
		}

		public void OnQuitGamePressed()
		{
			Dialog dialog = GameManager.Instance.GUIManager.CreateDialog ();
			dialog.SetHeadline ( "Quit game" );
			dialog.SetText ( "Are you sure you want to quit game?" );
			dialog.SetOKButtonText ( "Yes" );
			dialog.SetCancelButtonText ( "No" );
			dialog.SetOnOKClicked ( Application.Quit );
			dialog.SetOnCancelClicked ();

			dialog.Show ();
		}
	}
}
