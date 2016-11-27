using UnityEngine;
using System;

namespace GameProgramming3D.WaypointSystem
{
	public class PathUser : MonoBehaviour, IPathUser
	{
		#region Unity fields
		[SerializeField]
		private WaypointSystem _waypointSystem;
		[SerializeField]
		private Direction _direction;
		[SerializeField]
		private float _arriveDistance;
		[SerializeField]
		private float _speed;
		#endregion Unity fields

		private float _sqrArriveDistance;
		private Collider _collider;

		#region Properties
		public Waypoint CurrentWaypoint { get; private set; }

		public Direction Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}

		public Vector3 Position
		{
			get { return transform.position; }
			set { transform.position = value; }
		}
		#endregion Properties

		#region Unity messages
		protected void Awake()
		{
			_collider = GetComponent<Collider> ();
			// It is cheaper to calculate square than square root
			_sqrArriveDistance = _arriveDistance * _arriveDistance;
			CurrentWaypoint = _waypointSystem.GetClosestWaypoint ( Position );
		}

		protected void Update()
		{
			CurrentWaypoint = GetWaypoint ();
			Move ();
			Rotate ();
		}
		#endregion Unity messages

		private void Move()
		{
			Vector3 direction = CurrentWaypoint.Position - Position;
			Vector3 normalizedDirection = direction.normalized; // Length == 1
			Vector3 newPosition = 
				Position + normalizedDirection * _speed * Time.deltaTime;

			newPosition = Yield ( newPosition, normalizedDirection );

			Position = newPosition;
		}

		private Vector3 Yield ( Vector3 position, Vector3 normalizedDirection )
		{
			int mask = 1 << LayerMask.NameToLayer ( "PathUser" );
			//mask |= 1 << LayerMask.NameToLayer ( "Ground" );
			//int mask = LayerMask.GetMask("PathUser", "Ground");

			Ray ray = new Ray ( position, normalizedDirection );
			RaycastHit hitInfo;
			if(Physics.SphereCast(ray, _collider.bounds.size.x, out hitInfo, 5, mask))
			{
				IPathUser otherPathUser = hitInfo.collider.GetComponent<IPathUser> ();
				if(otherPathUser.Direction != Direction)
				{
					position += transform.right * _speed * Time.deltaTime;
				}
			}

			return position;
		}

		private void Rotate()
		{
			Vector3 direction = CurrentWaypoint.Position - Position;
			Vector3 normalizedDirection = direction.normalized;
			Vector3 rotation = Vector3.RotateTowards ( transform.forward,
				normalizedDirection, _speed * Time.deltaTime, 0 );
			transform.rotation = Quaternion.LookRotation ( rotation );
		}

		/// <summary>
		/// Gets the waypoint we are moving towards this frame
		/// </summary>
		/// <returns></returns>
		private Waypoint GetWaypoint()
		{
			Waypoint result = null;
			Vector3 toWaypointVector = CurrentWaypoint.Position - Position;
			float waypointVectorSqrMagnitude = toWaypointVector.sqrMagnitude;
			if(waypointVectorSqrMagnitude <= _sqrArriveDistance)
			{
				result = _waypointSystem.GetNextWaypoint ( CurrentWaypoint, ref _direction );
			}
			else
			{
				result = CurrentWaypoint;
			}

			return result;
		}
	}
}
