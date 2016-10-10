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
		private UnitCommand _aButtonCommand;
		private UnitCommand _bButtonCommand;
		private UnitCommand _xButtonCommand;
		private UnitCommand _yButtonCommand;
		private CommandBase _rKeyCommand;

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
			_aButtonCommand = new ShootUnitCommand();
			_yButtonCommand = new MoveBarrelCommand();
			_bButtonCommand = new MoveBarrelCommand();
			_rKeyCommand = new SaveGameCommand ();
		}

		protected void Update()
		{
			var unit = GameManager.Instance.SelectedUnit;

			HandleKeyDown ( KeyCode.R, _rKeyCommand );

			HandleUnitCommands ( unit );
		}

		private void HandleUnitCommands ( Unit unit )
		{
			if ( unit == null )
			{
				return;
			}

			HandleAxis ( "Horizontal", _horizontalCommand, unit );
			HandleAxis ( "Vertical", _verticalCommand, unit );

			HandleKeyDown ( KeyCode.Space, _spaceCommand, unit, 1 );
			HandleKey ( KeyCode.Q, _qKeyCommand, unit, -1 );
			HandleKey ( KeyCode.E, _eKeyCommand, unit, 1 );

			HandleGamepadButtonDown ( "A", _aButtonCommand, unit, 1 );
			HandleGamepadButton ( "B", _bButtonCommand, unit, -1 );
			HandleGamepadButton ( "Y", _yButtonCommand, unit, 1 );
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

		private void HandleKeyDown(KeyCode keyCode, CommandBase command)
		{
			if(Input.GetKeyDown(keyCode))
			{
				command.Execute ();
			}
		}

		private void HandleGamepadButtonDown( string buttonName, UnitCommand command, Unit unit, float amount )
		{
			var buttonDown = Input.GetButtonDown( buttonName );
			if ( buttonDown && command != null )
			{
				command.Execute(unit, amount);
			}
		}

		private void HandleGamepadButton ( string buttonName, UnitCommand command, Unit unit, float amount )
		{
			var buttonDown = Input.GetButton ( buttonName );
			if ( buttonDown && command != null )
			{
				command.Execute ( unit, amount );
			}
		}
	}
}
