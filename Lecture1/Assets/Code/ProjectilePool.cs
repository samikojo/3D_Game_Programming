using UnityEngine;

namespace GameProgramming3D.Utility
{
	public class ProjectilePool : GenericPool<Projectile>
	{
		protected override void Deactive( Projectile obj )
		{
			obj.Rigidbody.velocity = Vector3.zero;
			obj.gameObject.SetActive( false );
		}
	}
}
