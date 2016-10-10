using UnityEngine;
using System.Collections.Generic;

namespace GameProgramming3D
{
	public class Player : MonoBehaviour
	{
		private Unit[] _units;
		private int _unitIndex = -1;

		[SerializeField] private int _id;

		public int Id { get { return _id; } }

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

			if ( !CheckUnitIds() )
			{
				Debug.LogError( 
					string.Format( "Player {0}: There are invalid unit ids!", Id ) );
				Debug.Break();
			}
		}

		private bool CheckUnitIds()
		{
			HashSet<int> unitIds = new HashSet< int >();
			foreach ( Unit unit in _units )
			{
				if ( !unitIds.Add( unit.Id ) )
				{
					return false;
				}
			}

			return true;
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

		public void UnitKilled ()
		{
			bool anyUnitsAlive = false;
			foreach ( var unit in _units )
			{
				if ( unit.IsAlive )
				{
					anyUnitsAlive = true;
				}
			}

			if ( !anyUnitsAlive )
			{
				GameManager.Instance.PlayerLost();
			}
		}
	}
}
