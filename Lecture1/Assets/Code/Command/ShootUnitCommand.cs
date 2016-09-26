namespace GameProgramming3D.Command
{
	public class ShootUnitCommand : UnitCommand
	{
		public override void Execute ( Unit unit, float amount )
		{
			unit.Shoot ();
		}
	}
}
