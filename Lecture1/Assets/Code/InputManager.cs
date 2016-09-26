using UnityEngine;
using System.Collections;
using GameProgramming3D.Command;

namespace GameProgramming3D
{
	public class InputManager : MonoBehaviour
	{
		private UnitCommand _horizontalCommand;
		private UnitCommand _verticalCommand;
		private UnitCommand _spaceCommand;
		private UnitCommand _qKeyCommand;
		private UnitCommand _eKeyCommand;

		public void Init()
		{
			SetDefaultCommands ();
		}

		private void SetDefaultCommands()
		{
			_horizontalCommand = new TurnUnitCommand ();
			_verticalCommand = new MoveUnitCommand ();
			_spaceCommand = new ShootUnitCommand ();
			_eKeyCommand = new MoveBarrelCommand ();
			_qKeyCommand = new MoveBarrelCommand ();
		}

		protected void Update()
		{
			var unit = GameManager.Instance.SelectedUnit;

			HandleAxis ( "Horizontal", _horizontalCommand, unit );
			HandleAxis ( "Vertical", _verticalCommand, unit );

			HandleKeyDown ( KeyCode.Space, _spaceCommand, unit, 1 );
			HandleKey ( KeyCode.Q, _qKeyCommand, unit, -1 );
			HandleKey ( KeyCode.E, _eKeyCommand, unit, 1 );
		}

		private void HandleAxis(string axisName, 
			UnitCommand command, Unit unit)
		{
			var axisAmount = Input.GetAxis ( axisName );
			if(axisAmount != 0 && command != null)
			{
				command.Execute ( unit, axisAmount );
			}
		}

		private void HandleKey(KeyCode keyCode, 
			UnitCommand command, Unit unit, float amount)
		{
			var isKey = Input.GetKey ( keyCode );
			if(isKey && command != null)
			{
				command.Execute ( unit, amount );
			}
		}

		private void HandleKeyDown( KeyCode keyCode,
			UnitCommand command, Unit unit, float amount )
		{
			var isKeyDown = Input.GetKeyDown ( keyCode );
			if(isKeyDown && command != null)
			{
				command.Execute ( unit, amount );
			}
		}
	}
}
