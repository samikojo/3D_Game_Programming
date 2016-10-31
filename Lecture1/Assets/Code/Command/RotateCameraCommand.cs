using GameProgramming3D.Messages;

namespace GameProgramming3D.Command
{
	public class RotateCameraCommand : CommandBase
	{
		public float Amount { get; set; }

		public override void Execute ()
		{
			GameManager.Instance.MessageBus.
				Publish ( new RotateCameraMessage ( Amount ) );
		}
	}
}
