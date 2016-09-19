namespace GameProgramming3D
{
	public class Tank : Vehicle
	{
		public override void Select( bool isSelected )
		{
			if ( isSelected )
			{
				OnUnitSelected();
			}
		}
	}
}
