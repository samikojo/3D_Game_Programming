using System;
using WS = GameProgramming3D.WaypointSystem;
using UnityEngine;
using GameProgramming3D.Utility;

namespace GameProgramming3D.AI
{
	public class PatrolState : AIStateBase
	{
		private WS.WaypointSystem _path;
		private WS.Direction _direction;
		private float _sqrArriveDistance;

		public WS.Waypoint CurrentWaypoint { get; private set; }
		public WS.Direction Direction { get { return _direction; } }

		public PatrolState(Enemy owner, WS.WaypointSystem path,
			WS.Direction direction, float arriveDistance )
		{
			Owner = owner;
			State = AIStateType.Patrol;
			AddTransition ( AIStateTransition.PatrolToFollowTarget,
				AIStateType.FollowTarget );
			_direction = direction;
			_path = path;
			_sqrArriveDistance = arriveDistance * arriveDistance;
		}

		public override void StateActivated ()
		{
			base.StateActivated ();
			CurrentWaypoint = _path.GetClosestWaypoint ( Position );
		}

		public override void Update ()
		{
			if(!ChangeState())
			{
				// Update waypoint
				CurrentWaypoint = GetWaypoint ();
				// Rotate Owner
				Owner.Mover.Rotate ( CurrentWaypoint.Position );
				// Move owner
				Owner.Mover.Move ( CurrentWaypoint.Position );
			}
		}

		private WS.Waypoint GetWaypoint ()
		{
			WS.Waypoint result = CurrentWaypoint;
			Vector3 toWaypointVector = CurrentWaypoint.Position - Position;
			float sqrMagnitude = toWaypointVector.sqrMagnitude;
			if(sqrMagnitude <= _sqrArriveDistance)
			{
				result = _path.GetNextWaypoint ( CurrentWaypoint, ref _direction );
			}

			return result;
		}

		private bool ChangeState()
		{
			int playerMask = Flags.CreateMask ( LayerMask.NameToLayer ( "Player" ) );
			Collider[] players = Physics.OverlapSphere ( Position,
				Owner.DetectTargetDistance, playerMask );
			if(players.Length > 0)
			{
				Player player = players[0].GetComponentInParent<Player> ();
				Owner.Target = player;
				Owner.PerformTransition ( AIStateTransition.PatrolToFollowTarget );
				return true;
			}
			return false;
		}
	}
}
