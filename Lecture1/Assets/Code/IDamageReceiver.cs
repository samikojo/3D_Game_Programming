using UnityEngine;

namespace GameProgramming3D
{
	public interface IDamageReceiver
	{
		void TakeDamage( float amount );
		void ApplyExplosionForce ( float force,
			Vector3 position, float radius );
	}
}
