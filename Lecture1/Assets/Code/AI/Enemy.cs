using UnityEngine;
using System.Collections.Generic;
using GameProgramming3D.WaypointSystem;
using GameProgramming3D.Utility;
using System;

namespace GameProgramming3D.AI
{
	public class Enemy : MonoBehaviour, IShooter
	{
		[SerializeField] private WaypointSystem.WaypointSystem _path;
		[SerializeField] private Direction _direction;
		[SerializeField] private float _arriveDistance;
		[SerializeField] private float _speed;
		[SerializeField] private float _detectEnemyDistance;
		[SerializeField] private float _shootingDistance;
		[SerializeField] private float _shootInterval; // In seconds
		[SerializeField] private Transform _shootingPoint;
		[SerializeField] private ProjectilePool _projectiles;
		[SerializeField] private float _shootingForce;

		private List<AIStateBase> _states = new List<AIStateBase> ();
		private Collider _collider;

		#region Properties
		public AIStateBase CurrentState { get; private set; } // Current state of enemys state system
		public Player Target { get; set; } // The player object this enemy targets
		public float DetectTargetDistance { get { return _detectEnemyDistance; } }
		public float ShootingDistance { get { return _shootingDistance; } }
		public Mover Mover { get; private set; }
		#endregion Properties

		#region Unity messages
		protected void Awake()
		{
			InitDependencies ();
			InitStates ();
		}

		protected void Update()
		{
			CurrentState.Update ();
		}
		#endregion Unity messages

		private void InitStates()
		{
			PatrolState patrolState = 
				new PatrolState ( this, _path, _direction, _arriveDistance );
			FollowTargetState followTargetState = new FollowTargetState ( this );
			ShootState shootState = new ShootState ( this, _shootInterval );

			_states.Add ( patrolState );
			_states.Add ( followTargetState );
			_states.Add ( shootState );

			CurrentState = GetStateByType ( AIStateType.Patrol );
			CurrentState.StateActivated ();
		}

		private AIStateBase GetStateByType(AIStateType stateType)
		{
			// Same with Linq (System.Linq)
			// return _states.FirstOrDefault( state => state.State == stateType );

			foreach ( AIStateBase state in _states)
			{
				if(state.State == stateType)
				{
					return state;
				}
			}
			return null;
		}

		private void InitDependencies()
		{
			_collider = GetComponentInChildren<Collider> ();
			Mover = gameObject.GetOrAddComponent<Mover> ();
			Mover.Init ( _collider, _speed, _direction );
		}

		public void Shoot ()
		{
			Vector3 shootingDirection = _shootingPoint.forward;
			Projectile projectile = _projectiles.GetPooledObject ();
			projectile.transform.position = _shootingPoint.position;
			projectile.Shoot ( shootingDirection * _shootingForce, this );
		}

		public void PerformTransition(AIStateTransition transition)
		{
			AIStateType targetStateType = CurrentState.GetTargetStateType ( transition );
			if(targetStateType == AIStateType.Error)
			{
				return;
			}

			foreach(AIStateBase state in _states)
			{
				if(state.State == targetStateType)
				{
					CurrentState.StateDeactivating ();
					CurrentState = state;
					CurrentState.StateActivated ();
				}
			}
		}

		public void ProjectileHit ( Projectile projectile )
		{
			if(Target.Health.TakeDamage ( projectile.ExplosionDamage ))
			{
				Target.Health.Die ();
			}

			projectile.gameObject.SetActive ( false );
			_projectiles.ReturnObjectToPool ( projectile );
		}
	}
}
