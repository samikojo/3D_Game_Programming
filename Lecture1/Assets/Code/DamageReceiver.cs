using UnityEngine;
using System.Collections;

namespace GameProgramming3D
{
	public class DamageReceiver : MonoBehaviour, IDamageReceiver
	{
		[SerializeField]
		private float _health = 10;

		public void TakeDamage()
		{
			_health = Mathf.Max( 0, _health - 1 );
			if ( _health == 0 )
			{
				Destroy( gameObject );
			}
		}
	}
}
