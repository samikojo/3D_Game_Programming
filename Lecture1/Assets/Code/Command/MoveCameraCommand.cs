using UnityEngine;
using GameProgramming3D.Messages;

namespace GameProgramming3D.Command
{
	public class MoveCameraCommand : CommandBase
	{
		public Vector2 Amount { get; set; }

		public override void Execute ()
		{
			GameManager.Instance.MessageBus.
				Publish ( new MoveCameraMessage ( Amount ) );
		}
	}
}
