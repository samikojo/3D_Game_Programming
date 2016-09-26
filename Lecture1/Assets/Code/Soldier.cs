using UnityEngine;
using System.Collections;
using System;

namespace GameProgramming3D
{
	public class Soldier : Unit
	{
		[SerializeField]
		private float _jumpTime;

		private bool _isJumping = false;

		public CharacterController Controller { get; protected set; }

		protected override float Gravity
		{
			get
			{
				if ( _isJumping )
					return -GRAVITY;
				else
					return GRAVITY;
			}
		}

		protected override void Awake ()
		{
			base.Awake ();

			Controller = gameObject.GetOrAddComponent<CharacterController> ();
		}

		protected override void UpdateMovement ()
		{
			float y = MovementVector.y;
			y -= Gravity * Time.deltaTime; // y = y - 9.81f;
			MovementVector = new Vector3 ( MovementVector.x, y, MovementVector.z );
			if ( Controller.enabled )
			{
				Controller.Move ( MovementVector );
			}

			ResetMovement ();
		}

		private IEnumerator Jump()
		{
			_isJumping = true;
			yield return new WaitForSeconds( _jumpTime );
			_isJumping = false;
		}
	}
}
