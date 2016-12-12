using UnityEngine;

namespace GameProgramming3D.AI
{
	public class ShootCommand : PlayerCommandBase
	{
		private Vector3 _targetPoint;
		private Transform _shootingPoint;
		private float _shootingInterval;
		private float _timeElapsed = 0;

		public ShootCommand ( Player player, Vector3 position,
			float shootingInterval, Transform shootingPoint) : base ( player )
		{
			_targetPoint = position;
			_shootingPoint = shootingPoint;
			_shootingInterval = shootingInterval;
		}

		public override void Begin ()
		{
			base.Begin ();
			Vector3 direction = _targetPoint - AssociatedPlayer.Position;
			direction = Vector3.ProjectOnPlane ( direction, Vector3.up );
			direction = direction.normalized;
			Vector3 shootingPosition = AssociatedPlayer.Position + direction;
			shootingPosition.y = 1;
			_shootingPoint.position = shootingPosition;
			AssociatedPlayer.Shooter.Shoot ( _targetPoint );
		}

		public override void Update ()
		{
			base.Update ();
			_timeElapsed += Time.deltaTime;
			if(_timeElapsed >= _shootingInterval)
			{
				End ();
			}
		}
	}
}
