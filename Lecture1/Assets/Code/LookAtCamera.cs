using UnityEngine;
using System.Collections;

namespace GameProgramming3D.Utility
{
	[ExecuteInEditMode]
	public class LookAtCamera : MonoBehaviour
	{
		[SerializeField] private Camera _camera;

		protected void Awake()
		{
			if ( _camera == null )
			{
				// The first active camera is loaded scene which is tagged with "MainCamera" tag
				_camera = Camera.main;
			}
		}

		protected void Update()
		{
			Quaternion lookRotation = 
				Quaternion.LookRotation( _camera.transform.forward, _camera.transform.up );
			transform.rotation = lookRotation;
		}
	}
}
