using GameProgramming3D.Utility;
using UnityEngine;

namespace GameProgramming3D.AI
{
	public class FollowTargetState : AIStateBase
	{
		public FollowTargetState(Enemy owner)
		{
			Owner = owner; // The enemy object which owns this state
			State = AIStateType.FollowTarget;
			AddTransition ( AIStateTransition.FollowTargetToPatrol,
				AIStateType.Patrol );
			AddTransition ( AIStateTransition.FollowTargetToShoot,
				AIStateType.ShootTarget );
		}

		public override void Update ()
		{
			if(!ChangeState())
			{
				Owner.Mover.Rotate ( Owner.Target.Position );
				Owner.Mover.Move ( Owner.Target.Position );
			}
		}

		private bool ChangeState ()
		{
			// Is any enemy in shooting range
			int playerMask = Flags.CreateMask ( LayerMask.NameToLayer ( "Player" ) );
			Collider[] players = Physics.OverlapSphere ( Position,
				Owner.ShootingDistance, playerMask );
			foreach (Collider collider in players)
			{
				Player player = collider.GetComponentInParent<Player> ();
				float distanceToPlayer = Vector3.Distance ( player.Position, Position );
				if(distanceToPlayer < Owner.ShootingDistance)
				{
					Owner.Target = player;
					Owner.PerformTransition ( AIStateTransition.FollowTargetToShoot );
					return true;
				}
			}

			players = Physics.OverlapSphere ( Position,
				Owner.DetectTargetDistance, playerMask );
			if(players.Length == 0)
			{
				Owner.Target = null;
				Owner.PerformTransition ( AIStateTransition.FollowTargetToPatrol );
				return true;
			}

			return false;
		}
	}
}
