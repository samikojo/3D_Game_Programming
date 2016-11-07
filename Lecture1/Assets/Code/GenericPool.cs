using UnityEngine;
using System.Collections.Generic;

namespace GameProgramming3D.Utility
{
	public abstract class GenericPool<T> : MonoBehaviour
		where T : Component
	{
		[SerializeField] private int _objectAmount;
		[SerializeField] private T _objectPrefab;
		[SerializeField] private bool _shouldGrow;

		private List<T> _pool;
		private List< bool > _isActive;

		protected void Awake()
		{
			_pool = new List< T >(_objectAmount);
			_isActive = new List< bool >(_objectAmount);

			for ( int i = 0; i < _objectAmount; ++i )
			{
				AddItemToPool ( false );
			}
		}

		public T GetPooledObject()
		{
			T result = null;

			for ( int i = 0; i < _isActive.Count; ++i )
			{
				if ( !_isActive[ i ] )
				{
					result = _pool[ i ];
					_isActive[ i ] = true;
					break;
				}
			}

			if ( result == null && _shouldGrow )
			{
				result = AddItemToPool( true );
			}

			return result;
		}

		public void ReturnObjectToPool( T obj )
		{
			for ( int i = 0; i < _pool.Count; ++i )
			{
				if ( _pool[ i ] == obj )
				{
					Deactive( obj );
					_isActive[ i ] = false;
					break;
				}
			}
		}

		private T AddItemToPool ( bool activate )
		{
			T obj = Instantiate ( _objectPrefab );
			Deactive ( obj );
			_pool.Add ( obj );
			_isActive.Add ( activate );
			return obj;
		}

		protected abstract void Deactive( T obj );
	}
}
