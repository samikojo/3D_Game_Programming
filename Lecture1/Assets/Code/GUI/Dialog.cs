using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

namespace GameProgramming3D.GUI
{
	public class Dialog : MonoBehaviour
	{
		#region Private fields
		[SerializeField] private Text _headline;
		[SerializeField] private Text _text;
		[SerializeField] private Button _okButton;
		[SerializeField] private Button _cancelButton;
		[SerializeField] private Image _background;

		// UnityAction is a zero argument delegate.
		private bool _showCancel;
		private UnityAction _okButtonClick;
		private UnityAction _cancelButtonClick;
		private Vector3 _okButtonPosition; // OK Buttons original local position
		#endregion Private fields

		#region Unity messages
		private void Awake()
		{
			// Store OK buttons original position so that we can restore it later
			_okButtonPosition = _okButton.transform.localPosition;
			gameObject.SetActive ( false );
		}
		# endregion Unity messages

		#region Public interface

		/// <summary>
		/// Shows the dialog. Should be initialized before calling this.
		/// </summary>
		public void Show()
		{
			int screenWidth = Screen.width;
			Vector3 position = transform.localPosition;
			position.x -= ( screenWidth / 2 +
				_background.rectTransform.rect.width / 2 );
			transform.localPosition = position;

			var tweener = DOTween.To(() => 
				transform.localPosition, 
				(value) => transform.localPosition = value,
				Vector3.zero,
				0.5f
				);

			tweener.SetEase ( Ease.InOutCubic );
			tweener.SetUpdate ( true );
			
			gameObject.SetActive ( true );
		}

		/// <summary>
		/// Sets the text of headline
		/// </summary>
		public void SetHeadline(string text)
		{
			_headline.text = text;
		}

		/// <summary>
		/// Sets the actual text of the dialog
		/// </summary>
		public void SetText(string text)
		{
			_text.text = text;
		}

		/// <summary>
		/// Sets the text of the OK button
		/// </summary>
		public void SetOKButtonText(string text)
		{
			SetButtonText ( _okButton, text );
		}

		/// <summary>
		/// Sets the text of the Cancel button
		/// </summary>
		public void SetCancelButtonText(string text)
		{
			SetButtonText ( _cancelButton, text );
		}

		/// <summary>
		/// Defines if this is one or two button dialog
		/// </summary>
		/// <param name="showCancel">If true, both buttons are shown. If false, only OK button is shown.</param>
		public void SetShowCancel(bool showCancel)
		{
			_showCancel = showCancel;
			_cancelButton.gameObject.SetActive ( _showCancel );

			if(_showCancel)
			{
				_okButton.transform.localPosition = _okButtonPosition;
			}
			else
			{
				Vector3 okPosition = _okButtonPosition;
				okPosition.x = -_background.rectTransform.rect.width / 2;
				var rectTransform = _okButton.GetComponent<RectTransform> ();
				rectTransform.anchoredPosition = okPosition;
			}
		}

		public void CloseDialog(System.Action dialogClosedDelegate = null,
			bool destroyAfterClose = true)
		{
			int screenWidth = Screen.width;
			Vector3 position = transform.localPosition;
			position.x += screenWidth / 2 +
				_background.rectTransform.rect.width / 2;
			var tweener = DOTween.To (
				() => transform.localPosition,
				( value ) => transform.localPosition = value,
				position, 0.5f );
			tweener.SetEase ( Ease.InOutCubic );
			tweener.SetUpdate ( true );
			tweener.OnComplete ( () => 
			{
				OnDialogClosed ( dialogClosedDelegate );
				if ( destroyAfterClose )
				{
					Destroy ( gameObject );
				}
				else
				{
					gameObject.SetActive ( false );
					transform.localPosition = Vector3.zero;
				}
			} );
		}

		public void SetOnOKClicked( System.Action dialogClosedDelegate = null,
			bool destroyAfterClose = true )
		{
			_okButtonClick =
				() => CloseDialog ( dialogClosedDelegate, destroyAfterClose );
			SetOnClick ( _okButton, _okButtonClick );
		}

		public void SetOnCancelClicked( System.Action dialogClosedDelegate = null,
			bool destroyAfterClose = true )
		{
			_cancelButtonClick =
				() => CloseDialog ( dialogClosedDelegate, destroyAfterClose );
			SetOnClick ( _cancelButton, _cancelButtonClick );
		}

		#endregion Public interface

		#region Private methods

		private void OnDialogClosed(System.Action dialogClosedDelegate)
		{
			if(dialogClosedDelegate != null)
			{
				dialogClosedDelegate ();
			}
		}

		/// <summary>
		/// Sets text of a button. Note: Null checks should be added. If button is 
		/// null or doesn't have a label a a child, NullReferenceException is
		/// thrown.
		/// </summary>
		private void SetButtonText(Button button, string text)
		{
			Text label = button.GetComponentInChildren<Text> ();
			label.text = text;
		}

		/// <summary>
		/// Sets the action which is execured when button is clicked.
		/// </summary>
		/// <param name="callback">Reference to a method which should be executed when button is clicked.</param>
		private void SetOnClick(Button button, UnityAction callback)
		{
			button.onClick.AddListener ( callback );
		}
		#endregion Private methods
	}
}
