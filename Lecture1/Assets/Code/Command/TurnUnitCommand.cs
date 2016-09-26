namespace GameProgramming3D.Command
{
	public class TurnUnitCommand : UnitCommand
	{
		public override void Execute ( Unit unit, float amount )
		{
			unit.Turn ( amount );
		}
	}
}
