using UnityEngine;

namespace GameProgramming3D.Messages
{
	public class MoveCameraMessage : IMessage
	{
		public Vector2 Amount { get; private set; }

		public MoveCameraMessage(Vector2 amount)
		{
			Amount = amount;
		}
	}
}
