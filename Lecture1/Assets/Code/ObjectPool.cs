using UnityEngine;
using System.Collections.Generic;

namespace GameProgramming3D.Utility
{
	public class ObjectPool : MonoBehaviour
	{
		[SerializeField] private int _objectAmount;
		[SerializeField] private GameObject _objectPrefab;
		[SerializeField] private bool _shouldGrow;

		private List<GameObject> _pool;

		protected void Awake()
		{
			_pool = new List<GameObject> ( _objectAmount );
			for(int i = 0; i < _objectAmount; ++i )
			{
				AddObjectToPool ();
			}
		}

		public GameObject GetPooledObject()
		{
			GameObject result = null;

			for(int i = 0; i < _pool.Count; ++i )
			{
				GameObject obj = _pool[ i ];
				if ( !obj.activeInHierarchy )
				{
					result = obj;
					break;
				}
			}

			if ( result == null && _shouldGrow )
			{
				result = AddObjectToPool();
			}

			return result;
		}

		private GameObject AddObjectToPool ()
		{
			GameObject obj = Instantiate ( _objectPrefab );
			obj.SetActive ( false );
			_pool.Add ( obj );
			return obj;
		}
	}
}
