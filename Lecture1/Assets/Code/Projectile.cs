using UnityEngine;
using System.Collections;

namespace GameProgramming3D
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private float _blastRadius;
		[SerializeField] private float _explosionDamage;
		[SerializeField] private float _explosionForce;
		[SerializeField] private GameObject _explosionEffect;

		public Rigidbody Rigidbody { get { return _rigidbody; } }
		public float ExplosionDamage { get { return _explosionDamage; } }

		private Rigidbody _rigidbody;
		private IShooter _shooter;

		protected void Awake()
		{
			_rigidbody = gameObject.GetOrAddComponent<Rigidbody> ();
		}

		public void Shoot(Vector3 force, IShooter shooter)
		{
			_shooter = shooter;
			gameObject.SetActive ( true );
			_rigidbody.AddForce ( force, ForceMode.Impulse );
		}

		protected void OnCollisionEnter(Collision other)
		{
			Explode ();
		}
		
		private void Explode()
		{
			ApplyDamage ();
			InstantiateEffect ();
			_shooter.ProjectileHit( this );
		}

		private void InstantiateEffect()
		{
			var position = transform.position;
			position.y += 0.1f;
			var effect = Instantiate ( _explosionEffect,
				position, Quaternion.identity ) as GameObject;
			var effectDuration = 0f;
			var effects =
				effect.GetComponentsInChildren<ParticleSystem> ( true );

			foreach(var e in effects)
			{
				effectDuration = Mathf.Max ( effectDuration, e.duration );
			}

			Destroy ( effect, effectDuration );
		}

		private void ApplyDamage()
		{
			var damageReceivers = 
				Physics.OverlapSphere ( transform.position, _blastRadius );

			for (int i = 0; i < damageReceivers.Length; ++i )
			{
				var damageReceiver =
					damageReceivers[i].GetComponent<IDamageReceiver> ();
				if(damageReceiver != null)
				{
					damageReceiver.TakeDamage ( _explosionDamage );
					damageReceiver.ApplyExplosionForce ( _explosionForce,
						transform.position, _blastRadius );
				}
			}
		}
	}
}
