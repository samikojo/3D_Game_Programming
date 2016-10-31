namespace GameProgramming3D.Messages
{
	public class RotateCameraMessage : IMessage
	{
		public float Amount { get; private set; }

		public RotateCameraMessage(float amount)
		{
			Amount = amount;
		}
	}
}
