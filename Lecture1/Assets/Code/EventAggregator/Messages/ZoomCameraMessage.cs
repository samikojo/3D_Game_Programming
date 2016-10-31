namespace GameProgramming3D.Messages
{
	public class ZoomCameraMessage : IMessage
	{
		public enum ZoomDirection
		{
			In,
			Out
		};

		public ZoomDirection Direction { get; private set; }

		public ZoomCameraMessage ( ZoomDirection direction )
		{
			Direction = direction;
		}
	}
}
