using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using GameProgramming3D.Utility;

namespace GameProgramming3D.GUI
{
	public class BoosterGUI : MonoBehaviour
	{
		[SerializeField] private Toggle _togglePrefab;
		[SerializeField] private int _toggleHeight;

		private Dictionary< UnitBooster, BoosterToggle > _boosterToggles =
			new Dictionary< UnitBooster, BoosterToggle >();

		public void Init()
		{
			int index = 0;
			foreach ( UnitBooster boosterValue 
				in Enum.GetValues( typeof ( UnitBooster ) ) )
			{
				Toggle toggle = Instantiate( _togglePrefab );
				toggle.name = boosterValue.ToString();
				Text label = toggle.GetComponentInChildren< Text >();
				label.text = boosterValue.ToString();

				toggle.transform.SetParent( transform );
				Vector3 position = Vector3.zero;
				position.y -= ( _toggleHeight * index++ );
				toggle.transform.localPosition = position;
				toggle.transform.localScale = Vector3.one;

				BoosterToggle boosterToggle =
					toggle.gameObject.AddComponent< BoosterToggle >();
				boosterToggle.Init( boosterValue );
				_boosterToggles.Add( boosterValue, boosterToggle );
			}
		}

		public void UnitSelected( Unit unit )
		{
			foreach ( KeyValuePair<UnitBooster, BoosterToggle> kvp
				in _boosterToggles )
			{
				UnitBooster booster = kvp.Key;
				BoosterToggle toggle = kvp.Value;

				toggle.SetValue( Flags.Contains( unit.Boosters, booster ) );
			}
		}
	}
}
