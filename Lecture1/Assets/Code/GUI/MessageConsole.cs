using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace GameProgramming3D.GUI
{
	public class MessageConsole : MonoBehaviour
	{
		[SerializeField] private MessageItem _messageItemPrefab;
		[SerializeField] private float _yMargin;
		[SerializeField] private float _time;
		[SerializeField] private int _maxItems;

		private readonly List<MessageItem> _currentItems = new List< MessageItem >();
		private readonly Queue<MessageItem> _textQueue = new Queue< MessageItem >();

		public void AddText( string text )
		{
			var messageItem = Instantiate( _messageItemPrefab );
			messageItem.transform.SetParent( transform );
			messageItem.transform.localPosition = Vector3.zero;
			messageItem.SetItem( text, _time, this );

			if ( _currentItems.Count < _maxItems )
			{
				// Show item instantly
				ShowItem( messageItem );
			}
			else
			{
				_textQueue.Enqueue( messageItem );
			}
		}

		private void ShowItem( MessageItem item )
		{
			_currentItems.Add( item );
			var itemPosition = item.transform.localPosition;
			itemPosition.y -= ( _currentItems.Count - 1 ) * _yMargin;
			item.transform.localPosition = itemPosition;
			item.Show();
		}

		public void MessageHidden( MessageItem messageItem )
		{
			var itemIndex = _currentItems.IndexOf( messageItem );
			if ( itemIndex >= 0 && _currentItems.Remove ( messageItem ) )
			{
				for ( var i = itemIndex; i < _currentItems.Count; ++i )
				{
					var item = _currentItems[ i ];
					var itemPosition = item.transform.localPosition;
					itemPosition.y += _yMargin;
					item.transform.localPosition = itemPosition;
				}
				if ( _textQueue.Count > 0 )
				{
					var item = _textQueue.Dequeue();
					ShowItem( item );
				}
			}

			Destroy( messageItem.gameObject );
		}
	}
}
