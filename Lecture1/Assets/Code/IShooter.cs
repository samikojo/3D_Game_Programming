using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProgramming3D
{
	public interface IShooter
	{
		void Shoot();
		void ProjectileHit( Projectile projectile );
	}
}
