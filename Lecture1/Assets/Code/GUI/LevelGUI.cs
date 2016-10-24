using UnityEngine;
using UnityEngine.UI;

namespace GameProgramming3D.GUI
{
	public class LevelGUI : SceneGUI
	{
		private MessageConsole _messageConsole;

		protected void Awake ()
		{
			_messageConsole = GetComponentInChildren<MessageConsole> ( true );

			Unit.UnitSelected += HandleUnitSelected;
			Unit.UnitDied += HandleUnitDied;
		}

		protected void OnDestroy ()
		{
			Unit.UnitSelected -= HandleUnitSelected;
			Unit.UnitDied -= HandleUnitDied;
		}

		private void HandleUnitDied ( Unit unit )
		{
			_messageConsole.AddText ( string.Format ( "Unit {0} died.", unit.name ) );
		}

		private void HandleUnitSelected ( Unit unit )
		{
			_messageConsole.AddText ( "Unit " + unit.name + " selected." );
		}
	}
}
