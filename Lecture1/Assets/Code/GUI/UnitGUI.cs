using UnityEngine;

namespace GameProgramming3D.GUI
{
	public class UnitGUI : MonoBehaviour
	{
		private HealthBar _healthBar;

		public Unit AssociatedUnit { get; private set; }

		public void Init( Unit unit )
		{
			AssociatedUnit = unit;
			_healthBar = GetComponentInChildren< HealthBar >();
			_healthBar.Init( AssociatedUnit.Health );
			AssociatedUnit.DamageTaken += HandleDamageTaken;
			Unit.UnitDied += HandleUnitDied;
		}

		private void HandleUnitDied ( Unit unit )
		{
			if(unit == AssociatedUnit)
			{
				// When associated unit dies stop listening Unit.UnitDied and DamageTaken events.
				Unit.UnitDied -= HandleUnitDied;
				AssociatedUnit.DamageTaken -= HandleDamageTaken;
			}
		}

		private void HandleDamageTaken ( int health )
		{
			_healthBar.SetHealth ( health );
		}
	}
}
