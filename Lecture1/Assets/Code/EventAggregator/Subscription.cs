using System;

namespace GameProgramming3D.Messages
{
	public class Subscription<TMessage> : ISubscription<TMessage>
		where TMessage : IMessage
	{
		public Subscription(EventAggregator eventAggregator, Action<TMessage> action)
		{
			EventAggregator = eventAggregator;
			Action = action;
		}

		public Action<TMessage> Action { get; private set; }
		public EventAggregator EventAggregator { get; private set; }

		public void Dispose ()
		{
			EventAggregator.Unsubscribe ( this );
			GC.SuppressFinalize ( this );
		}
	}
}
