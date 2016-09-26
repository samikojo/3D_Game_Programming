namespace GameProgramming3D.Command
{
	public class MoveBarrelCommand : UnitCommand
	{
		public override void Execute ( Unit unit, float amount )
		{
			var tank = unit as Tank;
			if(tank == null)
			{
				return;
			}

			tank.MoveBarrel ( amount );
		}
	}
}
