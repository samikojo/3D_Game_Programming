namespace GameProgramming3D.Command
{
	public class PauseCommand : CommandBase
	{
		public override void Execute ()
		{
			GameManager.Instance.PauseGame ();
		}
	}
}
