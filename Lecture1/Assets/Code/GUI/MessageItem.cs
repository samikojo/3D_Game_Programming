using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameProgramming3D.GUI
{
	public class MessageItem : MonoBehaviour
	{
		[SerializeField] private Text _text;

		private float _time;
		private MessageConsole _messageConsole;
		private Coroutine _timerCoroutine;

		public void SetItem( string text, float time, MessageConsole messageConsole )
		{
			gameObject.SetActive( false );
			if ( _text == null )
			{
				_text = gameObject.GetOrAddComponent< Text >();
			}

			_text.text = text;
			_time = time;
			this._messageConsole = messageConsole;
		}

		public void Show()
		{
			gameObject.SetActive( true );
			_timerCoroutine = StartCoroutine( Timer( _time ) );
		}

		public void Hide()
		{
			gameObject.SetActive( false );
			if ( _timerCoroutine != null )
			{
				StopCoroutine( _timerCoroutine );
				_timerCoroutine = null;
			}
			_messageConsole.MessageHidden( this );
		}

		private IEnumerator Timer( float time )
		{
			yield return new WaitForSeconds( time );
			Hide();
		}
	}
}
