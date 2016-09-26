namespace GameProgramming3D.Command
{
	public class MoveUnitCommand : UnitCommand
	{
		public override void Execute ( Unit unit, float amount )
		{
			unit.Move ( amount );
		}
	}
}
