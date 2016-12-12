using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GameProgramming3D.AI
{
	public class MoveCommand : PlayerCommandBase
	{
		private Vector3 _targetPoint;
		private const float _arriveDistance = 0.5f;

		public MoveCommand ( Player player, Vector3 targetPoint )
			: base ( player )
		{
			_targetPoint = targetPoint;
		}

		public override void Update ()
		{
			AssociatedPlayer.Mover.Rotate ( _targetPoint );
			AssociatedPlayer.Mover.Move ( _targetPoint );
			if(Vector3.Distance(AssociatedPlayer.Position, _targetPoint) 
				<= _arriveDistance)
			{
				End ();
			}
		}
	}
}
