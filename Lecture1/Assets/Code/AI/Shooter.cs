using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GameProgramming3D.Utility;

namespace GameProgramming3D.AI
{
	public class Shooter : MonoBehaviour, IShooter
	{
		[SerializeField] private float _shootingForce;

		private Transform _shootingPoint;
		private ProjectilePool _projectilePool;

		public void Init(Transform shootingPoint)
		{
			_shootingPoint = shootingPoint;
			_projectilePool = gameObject.GetOrAddComponent<ProjectilePool> ();
		}

		public void ProjectileHit ( Projectile projectile )
		{
			projectile.gameObject.SetActive ( false );
			_projectilePool.ReturnObjectToPool ( projectile );
		}

		public void Shoot ()
		{
			DoShoot ( _shootingPoint.forward );
		}

		public void Shoot ( Vector3 position )
		{
			DoShoot ( position - _shootingPoint.position );
		}

		private void DoShoot(Vector3 shootingDirection)
		{
			Projectile projectile = _projectilePool.GetPooledObject ();
			if(projectile != null)
			{
				projectile.transform.position = _shootingPoint.position;
				projectile.Shoot ( shootingDirection.normalized * _shootingForce, this );
			}
		}
	}
}
