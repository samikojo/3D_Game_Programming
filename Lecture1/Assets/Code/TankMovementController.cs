using UnityEngine;
using System.Collections;

namespace GameProgramming3D
{
	public class TankMovementController : MonoBehaviour
	{
		public void Turn(float amount)
		{
			transform.Rotate ( Vector3.up, amount, Space.Self );
		}

		public void Move(float amount)
		{
			var position = transform.position;
			position += transform.forward * amount;
			transform.position = position;
		}
	}
}
