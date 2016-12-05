using System.Collections.Generic;
using UnityEngine;

namespace GameProgramming3D.AI
{
	public enum AIStateType
	{
		Error = 0,
		Patrol,
		FollowTarget,
		ShootTarget
	}

	public enum AIStateTransition
	{
		Error = 0,
		PatrolToFollowTarget,
		FollowTargetToShoot,
		FollowTargetToPatrol,
		ShootTargetToFollowTarget,
		ShootTargetToPatrol
	}

	public abstract class AIStateBase
	{
		public AIStateType State { get; protected set; }
		public Dictionary<AIStateTransition, AIStateType> TransitionStateMap { get; protected set; }

		public Enemy Owner { get; protected set; }

		public Vector3 Position
		{
			get { return Owner.transform.position; }
			set { Owner.transform.position = value; }
		}

		protected AIStateBase()
		{
			TransitionStateMap = new Dictionary< AIStateTransition, AIStateType >();
		}

		public void AddTransition( AIStateTransition transition, 
			AIStateType targetStateType )
		{
			TransitionStateMap.Add( transition, targetStateType );
		}

		public bool RemoveTransition( AIStateTransition transition )
		{
			return TransitionStateMap.Remove( transition );
		}

		public AIStateType GetTargetStateType( AIStateTransition transition )
		{
			if ( TransitionStateMap.ContainsKey( transition ) )
			{
				return TransitionStateMap[ transition ];
			}
			return AIStateType.Error;
		}

		public virtual void StateActivated () { }
		public virtual void StateDeactivating () { }
		public abstract void Update ();
	}
}
