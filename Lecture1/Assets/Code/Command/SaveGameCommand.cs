namespace GameProgramming3D.Command
{
	public class SaveGameCommand : CommandBase
	{
		public override void Execute ()
		{
			GameManager.Instance.SaveGame ();
		}
	}
}
