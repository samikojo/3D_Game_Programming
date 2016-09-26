using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameProgramming3D.GUI
{
	public class TurnTimer : MonoBehaviour
	{
		private Text _text;

		protected void Awake()
		{
			_text = gameObject.GetOrAddComponent<Text> ();
		}

		protected void Update()
		{
			var time = GameManager.Instance.TurnTimer.CurrentTime;
			var minutes = (int)(time / 60);
			var seconds = (int)(time % 60);

			_text.text = string.Format ( "{0}:{1}",
				minutes.ToString("D2"), seconds.ToString ( "D2" ) );
		}
	}
}
