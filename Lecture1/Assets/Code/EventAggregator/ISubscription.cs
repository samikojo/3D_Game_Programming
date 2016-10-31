namespace GameProgramming3D.Messages
{
	public interface ISubscription<TMessage> : System.IDisposable
		where TMessage : IMessage
	{
		System.Action<TMessage> Action { get; }
		EventAggregator EventAggregator { get; }
	}
}
