using UnityEngine;
using UnityEngine.UI;

namespace GameProgramming3D.GUI
{
	public class LevelGUI : SceneGUI
	{
		private MessageConsole _messageConsole;
		private TotalHealths _totalHealths;
		private BoosterGUI _boosterGUI;

		public override void Init ()
		{
			_messageConsole = GetComponentInChildren<MessageConsole> ( true );
			_totalHealths = GetComponentInChildren<TotalHealths> ( true );
			_boosterGUI = GetComponentInChildren< BoosterGUI >( true );

			_totalHealths.Init( GameManager.Instance.AllPlayers );
			_boosterGUI.Init();

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
			_boosterGUI.UnitSelected( unit );
		}
	}
}
