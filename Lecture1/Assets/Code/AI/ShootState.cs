using System;
using UnityEngine;

namespace GameProgramming3D.AI
{
	public class ShootState : AIStateBase
	{
		private float _shootInterval; // How often we can shoot?
		private float _previouslyShot = float.PositiveInfinity; // How long it has been since we shot last time?

		public ShootState(Enemy owner, float shootInterval)
		{
			State = AIStateType.ShootTarget;
			AddTransition ( AIStateTransition.ShootTargetToFollowTarget,
				AIStateType.FollowTarget );
			AddTransition ( AIStateTransition.ShootTargetToPatrol, AIStateType.Patrol );
			Owner = owner;
			_shootInterval = shootInterval;
		}

		public override void Update ()
		{
			float distanceToTarget = 
				Vector3.Distance ( Owner.Target.Position, Position );
			if ( distanceToTarget > Owner.ShootingDistance)
			{
				Owner.PerformTransition ( AIStateTransition.ShootTargetToFollowTarget );
			}
			else
			{
				Owner.Mover.Rotate ( Owner.Target.Position );
				if(_previouslyShot >= _shootInterval)
				{
					// We can shoot!
					_previouslyShot = 0;
					Owner.Shoot ();
				}
				else
				{
					// We cannot shoot just yet. Instead, increase _previouslyShot timer.
					_previouslyShot += Time.deltaTime;
				}
			}
		}

		public override void StateActivated ()
		{
			base.StateActivated ();
			Owner.Target.Health.TargetDied += HandleTargetDied;
		}

		public override void StateDeactivating ()
		{
			base.StateDeactivating ();
			Owner.Target.Health.TargetDied -= HandleTargetDied;
		}

		private void HandleTargetDied ()
		{
			Owner.PerformTransition ( AIStateTransition.ShootTargetToPatrol );
			Owner.Target = null;
		}
	}
}
