using UnityEngine;
using System.Collections;

namespace GameProgramming3D
{
	public class Player : MonoBehaviour
	{
		private Unit[] _units;
		private int _unitIndex = -1;

		public Unit UnitInTurn
		{
			get
			{
				return _unitIndex >= 0 &&
					_unitIndex < _units.Length ? _units[_unitIndex] : null;
			}
		}

		public void Init()
		{
			_units = GetComponentsInChildren<Unit> ();
			for(int i = 0; i < _units.Length; i++ )
			{
				_units[i].Init ( this );
			}
		}
	
		public void StartTurn()
		{
			_unitIndex++;
			if(_unitIndex >= _units.Length)
			{
				_unitIndex = 0;
			}
			GameManager.Instance.SelectedUnit = UnitInTurn;
		}
	}
}
