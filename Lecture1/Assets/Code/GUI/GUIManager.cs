using System;
using GameProgramming3D.GUI;
using GameProgramming3D.State;
using GameProgramming3D.Exceptions;
using UnityEngine;
using UnityEngine.UI;

namespace GameProgramming3D
{
	public class GUIManager : MonoBehaviour
	{
		[SerializeField] private Dialog _dialogPrefab;

		public SceneGUI SceneGUI { get; private set; }

		private void OnEnable()
		{
			GameManager.Instance.StateManager.GameStateChanged += 
				HandleGameStateChanged;
			SceneGUI = FindObjectOfType<SceneGUI> ();
		}

		private void OnDisable()
		{
			GameManager.Instance.StateManager.GameStateChanged -=
				HandleGameStateChanged;
		}

		private void HandleGameStateChanged ( StateType type )
		{
			SceneGUI = FindObjectOfType<SceneGUI> ();
			if(SceneGUI == null)
			{
				Debug.LogWarning ( "Could not find SceneGUI object from loaded scene. " +
					"Is this intentional?" );
			}
		}

		public Dialog CreateDialog()
		{
			if(SceneGUI == null)
			{
				throw new StateGUINotFoundException ();
			}

			Dialog dialog = Instantiate ( _dialogPrefab );
			dialog.transform.SetParent ( SceneGUI.transform );
			dialog.transform.localPosition = Vector3.zero;
			dialog.transform.SetAsLastSibling ();

			return dialog;
		}
	}
}
