using GameProgramming3D.WaypointSystem;
using UnityEngine;

namespace GameProgramming3D.AI
{
	public class Mover : MonoBehaviour
	{
		private float _speed;
		private Collider _collider;
		private Direction _direction;

		public Vector3 Position
		{
			get { return transform.position; }
			set { transform.position = value; }
		}

		public void Init(Collider collider, float speed, Direction direction)
		{
			_speed = speed;
			_collider = collider;
			_direction = direction;
		}

		public void Move ( Vector3 targetPosition )
		{
			Vector3 direction = targetPosition - Position;
			Vector3 normalizedDirection = direction.normalized; // Length == 1
			Vector3 newPosition =
				Position + normalizedDirection * _speed * Time.deltaTime;

			newPosition = Yield ( newPosition, normalizedDirection );

			Position = newPosition;
		}

		private Vector3 Yield ( Vector3 position, Vector3 normalizedDirection )
		{
			int mask = 1 << LayerMask.NameToLayer ( "PathUser" );

			Ray ray = new Ray ( position, normalizedDirection );
			RaycastHit hitInfo;
			if ( Physics.SphereCast ( ray, _collider.bounds.size.x, out hitInfo, 5, mask ) )
			{
				IPathUser otherPathUser = hitInfo.collider.GetComponent<IPathUser> ();
				if ( otherPathUser.Direction != _direction )
				{
					position += transform.right * _speed * Time.deltaTime;
				}
			}

			return position;
		}

		public void Rotate ( Vector3 targetPosition )
		{
			Vector3 direction = targetPosition - Position;
			direction.y = Position.y;
			Vector3 normalizedDirection = direction.normalized;
			Vector3 rotation = Vector3.RotateTowards ( transform.forward,
				normalizedDirection, _speed * Time.deltaTime, 0 );
			transform.rotation = Quaternion.LookRotation ( rotation );
		}
	}
}
