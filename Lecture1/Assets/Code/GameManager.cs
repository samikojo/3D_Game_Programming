using UnityEngine;

namespace GameProgramming3D
{
	public class GameManager : MonoBehaviour
	{
		private Unit _selectedUnit;

		public Unit SelectedUnit
		{
			get { return _selectedUnit; }
			private set
			{
				if ( _selectedUnit != null )
				{
					_selectedUnit.Select( false );
				}
				_selectedUnit = value;
				if ( _selectedUnit != null )
				{
					_selectedUnit.Select( true );
				}
			}
		}

		public Unit[] AllUnits { get; private set; }

		protected void Awake()
		{
			AllUnits = FindObjectsOfType< Unit >();
		}

		protected void Update()
		{
			if ( Input.GetMouseButtonDown( 0 ) )
			{
				var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hit;
				if ( Physics.Raycast( ray, out hit, 50 ) )
				{
					var unit = hit.collider.GetComponent< Unit >();
					if ( unit != null )
					{
						SelectedUnit = unit;
					}
				}
			}

			if ( Input.GetMouseButtonDown( 1 ) )
			{
				var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hit;
				if ( Physics.Raycast( ray, out hit, 50 ) )
				{
					var damageReceiver = hit.collider.GetComponent< IDamageReceiver >();
					if ( damageReceiver != null )
					{
						damageReceiver.TakeDamage();
					}
				}
			}

			if ( SelectedUnit != null )
			{
				SelectedUnit.Move();
			}
		} // Update
	}
}
