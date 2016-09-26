using UnityEngine;
using System.Collections;
using System;

namespace GameProgramming3D
{
	public class DamageReceiver : MonoBehaviour, IDamageReceiver
	{
		[SerializeField]
		private float _health = 10;

		public void ApplyExplosionForce ( float force, Vector3 position, float radius )
		{
			if(_health > 0)
			{
				var rigidbody = GetComponent<Rigidbody> ();
				if(rigidbody != null)
				{
					rigidbody.AddExplosionForce ( force, position, radius );
				}
			}
		}

		public void TakeDamage( float amount )
		{
			_health = Mathf.Max( 0, _health - amount );
			if ( _health == 0 )
			{
				Destroy( gameObject );
			}
		}
	}
}
