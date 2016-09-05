using UnityEngine;
using System.Collections;

namespace GameProgramming3D
{
	public class Soldier : Unit
	{
		[SerializeField]
		private float _jumpTime;

		private bool _isJumping = false;

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

		public override void Move()
		{
			base.Move();

			var jump = Input.GetAxis( "Jump" );
			if ( Controller.isGrounded && jump > 0 )
			{
				StartCoroutine( Jump() );
			}
		}

		private IEnumerator Jump()
		{
			_isJumping = true;
			yield return new WaitForSeconds( _jumpTime );
			_isJumping = false;
		}
	}
}
