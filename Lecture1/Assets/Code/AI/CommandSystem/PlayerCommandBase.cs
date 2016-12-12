using System;

namespace GameProgramming3D.AI
{
	public abstract class PlayerCommandBase
	{
		public event Action<PlayerCommandBase> CommandExecuted;

		public Player AssociatedPlayer { get; private set; }

		protected PlayerCommandBase( Player player )
		{
			AssociatedPlayer = player;
		}

		public virtual void Begin() { }
		public virtual void Update() { }
		public virtual void End()
		{
			if(CommandExecuted != null)
			{
				CommandExecuted ( this );
			}
		}
	}
}
