using GameProgramming3D.Messages;

namespace GameProgramming3D.Command
{
	public class ZoomCameraCommand : CommandBase
	{
		public ZoomCameraMessage.ZoomDirection Direction { get; set; }

		public override void Execute ()
		{
			GameManager.Instance.MessageBus.
				Publish ( new ZoomCameraMessage ( Direction ) );
		}
	}
}
