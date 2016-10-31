using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

namespace GameProgramming3D.Messages
{
	public class EventAggregator
	{
		// Contains all the subscriptions
		private readonly IDictionary<Type, IList> _subscriptions =
			new Dictionary<Type, IList> ();

		// Publishes the message of type TMessage
		public void Publish<TMessage>(TMessage message)
			where TMessage : IMessage
		{
			Type messageType = typeof ( TMessage );
			if(_subscriptions.ContainsKey(messageType))
			{
				var subscriptionsList =
					new List<ISubscription<TMessage>> ( _subscriptions[messageType]
					.Cast<ISubscription<TMessage>> () );
				foreach ( var subscription in subscriptionsList )
				{
					subscription.Action ( message );
				}
			}
		}

		public ISubscription<TMessage> 
			Subscribe<TMessage>(System.Action<TMessage> action)
			where TMessage : IMessage
		{
			Type messageType = typeof ( TMessage );
			Subscription<TMessage> subscription = 
				new Subscription<TMessage> ( this, action );

			if(_subscriptions.ContainsKey(messageType))
			{
				_subscriptions[messageType].Add ( subscription );
			}
			else
			{
				var subscriptionList =
					new List<ISubscription<TMessage>> () { subscription };
				_subscriptions.Add ( messageType, subscriptionList );
			}

			return subscription;
		}

		public void Unsubscribe<TMessage>(ISubscription<TMessage> subscription)
			where TMessage : IMessage
		{
			Type messageType = typeof ( TMessage );
			if(_subscriptions.ContainsKey(messageType))
			{
				_subscriptions[messageType].Remove ( subscription );
			}
		}

		public void ClearAllSubscriptions()
		{
			ClearAllSubscriptions ( null );
		}

		public void ClearAllSubscriptions(System.Type[] exceptMessages)
		{
			foreach (var messageSubscriptions in 
				new Dictionary<Type, IList>(_subscriptions))
			{
				bool canDelete = true;
				if(exceptMessages != null)
				{
					canDelete = !exceptMessages.Contains ( messageSubscriptions.Key );
				}

				if(canDelete)
				{
					_subscriptions.Remove ( messageSubscriptions );
				}
			}
		}
	}
}
