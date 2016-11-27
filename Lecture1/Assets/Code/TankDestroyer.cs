using UnityEngine;
using System.Collections;

namespace GameProgramming3D
{
	public class TankDestroyer : MonoBehaviour
	{
		public Rigidbody Rigidbody { get; private set; }

		public void Init()
		{
			var meshCollider = gameObject.GetOrAddComponent< MeshCollider >();
			var meshFilter = GetComponent< MeshFilter >();
			if ( meshFilter != null )
			{
				meshCollider.sharedMesh = meshFilter.sharedMesh;
				meshCollider.convex = true;
			}

			Rigidbody = gameObject.GetOrAddComponent< Rigidbody >();
		}
	}
}
