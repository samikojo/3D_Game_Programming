namespace GameProgramming3D.Command
{
	public abstract class UnitCommand : CommandBase
	{
		public abstract void Execute ( Unit unit, float amount );

		public override void Execute ()
		{
		}
	}
}
