using UnityEngine;

namespace GameProgramming3D
{
	public interface IShooter
	{
		void Shoot();
		void Shoot ( Vector3 position );
		void ProjectileHit( Projectile projectile );
	}
}
