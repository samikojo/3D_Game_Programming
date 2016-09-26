using UnityEngine;

namespace GameProgramming3D
{
	public class Tank : Vehicle
	{
		[SerializeField] private GameObject _selectionCircle;
		[SerializeField] private Transform _shootingStartPoint;
		[SerializeField] private float _shootingForce;
		[SerializeField] private Projectile _projectilePrefab;
		[SerializeField] private Transform _barrel;
		[SerializeField] private float _barrelMoveSpeed;
		[SerializeField] private float _minBarrelAngle;
		[SerializeField] private float _maxBarrelAngle;

		private TankMovementController _movementController;

		public override void Init ( Player player )
		{
			base.Init ( player );
			_movementController =
				gameObject.GetOrAddComponent<TankMovementController> ();
		}

		public override void Select( bool isSelected )
		{
			_selectionCircle.SetActive ( isSelected );
			if ( isSelected )
			{
				OnUnitSelected();
			}
		}

		protected override void UpdateMovement ()
		{
			_movementController.Turn ( RotationAmount );
			_movementController.Move ( MoveAmount );
			ResetMovement ();
		}

		public override void Shoot ()
		{
			var projectile = Instantiate ( _projectilePrefab );
			projectile.transform.position = _shootingStartPoint.position;
			var shootingDirection = _shootingStartPoint.forward;
			projectile.Shoot ( shootingDirection * _shootingForce );
		}

		public override void Die ()
		{
			OnUnitDied ();
		}

		public void MoveBarrel(float amount)
		{
			var barrelRotation = _barrel.localEulerAngles;

			var barrelRotationDiff = 
				amount * _barrelMoveSpeed * Time.deltaTime;

			barrelRotation.x = Mathf.Clamp ( 
				barrelRotation.x + barrelRotationDiff, 
				_minBarrelAngle, _maxBarrelAngle );

			_barrel.localEulerAngles = barrelRotation;
		}
	}
}
