using System;

namespace GameProgramming3D.Exceptions
{
	public class StateGUINotFoundException : Exception
	{
		public override string Message
		{
			get
			{
				return "Could not find GameStateGUI from loaded scene!";
			}
		}
	}
}
