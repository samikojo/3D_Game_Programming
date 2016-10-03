using System;
using System.Collections.Generic;

namespace GameProgramming3D.State
{
	public abstract class StateBase
	{
		public StateType State { get; protected set; }
		protected Dictionary<TransitionType, StateType> TransitionStateMap { get; set; }

		protected StateBase()
		{
			TransitionStateMap = new Dictionary< TransitionType, StateType >();
		}

		public bool AddTransition( TransitionType transition, StateType targetState )
		{
			if ( transition == TransitionType.Error || targetState == StateType.Error )
			{
				return false;
			}

			if ( TransitionStateMap.ContainsKey( transition ) )
			{
				return false;
			}

			TransitionStateMap.Add( transition, targetState );
			return true;
		}

		public bool RemoveTransition( TransitionType transition )
		{
			return TransitionStateMap.Remove( transition );
		}

		public StateType GetTargetStateType( TransitionType transition )
		{
			if ( TransitionStateMap.ContainsKey( transition ) )
			{
				return TransitionStateMap[ transition ];
			}
			return StateType.Error;
		}

		public virtual void StateActivated()
		{
		}

		public virtual void StateDeactivating()
		{
		}
	}
}
