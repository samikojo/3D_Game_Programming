using UnityEngine;
using UnityEngine.UI;
using GameProgramming3D.Utility;

namespace GameProgramming3D.GUI
{
	public class BoosterToggle : MonoBehaviour
	{
		private Toggle _toggle;

		public UnitBooster Booster { get; private set; }

		public void Init( UnitBooster booster )
		{
			Booster = booster;
			_toggle = GetComponentInChildren< Toggle >();
			_toggle.onValueChanged.AddListener( HandleValueChanged );
		}

		public void SetValue( bool isOn )
		{
			_toggle.isOn = isOn;
		}

		private void HandleValueChanged( bool isOn )
		{
			Unit unit = GameManager.Instance.SelectedUnit;
			if ( isOn )
			{
				unit.Boosters = Flags.Set ( unit.Boosters, Booster );
			}
			else
			{
				unit.Boosters = Flags.Unset ( unit.Boosters, Booster );
			}
		}
	}
}
