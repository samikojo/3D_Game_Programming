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
		private CommandBase _pKeyCommand;

		private MoveCameraCommand _moveCameraCommand;
		private RotateCameraCommand _rotateCameraCommand;
		private ZoomCameraCommand _zoomCameraCommand;

		private bool _isMovingCamera = false;
		private bool _isRotatingCamera = false;
		private Vector3 _mousePosition;

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
			_pKeyCommand = new PauseCommand ();

			_moveCameraCommand = new MoveCameraCommand ();
			_rotateCameraCommand = new RotateCameraCommand ();
			_zoomCameraCommand = new ZoomCameraCommand ();
		}

		protected void Update()
		{
			var unit = GameManager.Instance.SelectedUnit;

			HandleCameraCommands ();

			HandleKeyDown ( KeyCode.R, _rKeyCommand );
			HandleKeyDown ( KeyCode.P, _pKeyCommand );

			HandleUnitCommands ( unit );
		}

		private void HandleCameraCommands()
		{
			HandleMoveCamera ();
			HandleRotateCamera ();
			HandleZoomCamera ();
		}

		private void HandleZoomCamera ()
		{
			float mouseWheel = Input.GetAxis ( "Mouse ScrollWheel" );
			if ( mouseWheel != 0 )
			{
				_zoomCameraCommand.Direction = mouseWheel < 0
					? Messages.ZoomCameraMessage.ZoomDirection.Out
					: Messages.ZoomCameraMessage.ZoomDirection.In;
				_zoomCameraCommand.Execute ();
			}
		}

		private void HandleRotateCamera ()
		{
			if ( _isRotatingCamera && Input.GetMouseButton ( 1 ) )
			{
				Vector3 mousePosition = Input.mousePosition;
				Vector3 mouseDifference = _mousePosition - mousePosition;
				_mousePosition = mousePosition;

				_rotateCameraCommand.Amount = mouseDifference.x;
				_rotateCameraCommand.Execute ();
			}

			if ( Input.GetMouseButtonDown ( 1 ) )
			{
				_isRotatingCamera = true;
				_mousePosition = Input.mousePosition;
			}

			if ( Input.GetMouseButtonUp ( 1 ) )
			{
				_isRotatingCamera = false;
			}
		}

		private void HandleMoveCamera ()
		{
			if ( _isMovingCamera && Input.GetMouseButton ( 0 ) )
			{
				Vector3 mousePosition = Input.mousePosition;
				Vector3 mouseDifference = _mousePosition - mousePosition;
				_mousePosition = mousePosition;
				_moveCameraCommand.Amount = mouseDifference;
				_moveCameraCommand.Execute ();
			}

			if ( Input.GetMouseButtonDown ( 0 ) )
			{
				_isMovingCamera = true;
				_mousePosition = Input.mousePosition;
			}

			if ( Input.GetMouseButtonUp ( 0 ) )
			{
				_isMovingCamera = false;
			}
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
