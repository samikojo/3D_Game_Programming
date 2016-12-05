using UnityEngine;
using System;

namespace GameProgramming3D.AI
{
	public class Health : MonoBehaviour
	{
		[SerializeField]
		private float _health = 10;

		public event Action TargetDied;

		public bool TakeDamage(float damage)
		{
			_health = Mathf.Clamp ( _health - damage, 0, _health );
			return _health <= 0;
		}

		public void Die()
		{
			if ( TargetDied != null )
			{
				TargetDied ();
			}
			gameObject.SetActive ( false );
		}
	}
}
