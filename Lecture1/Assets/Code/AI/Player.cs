using UnityEngine;
using System.Collections;
using System;

namespace GameProgramming3D.AI
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private Health _health;

		public Vector3 Position { get { return transform.position; } }
		public Health Health { get { return _health; } }

		protected void Awake()
		{
			if(_health == null)
			{
				_health = gameObject.GetOrAddComponent<Health> ();
			}
		}

		protected void Update()
		{
			float vertical = Input.GetAxis ( "Vertical" );
			float horizontal = Input.GetAxis ( "Horizontal" );
			Vector3 position = Position;
			position += transform.forward * vertical * _speed * Time.deltaTime;
			position += transform.right * horizontal * _speed * Time.deltaTime;
			transform.position = position;
		}

		public void ApplyExplosionForce ( float force, Vector3 position, float radius )
		{	}
	}
}
