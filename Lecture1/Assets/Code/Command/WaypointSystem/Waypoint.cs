using UnityEngine;
using System.Collections;

namespace GameProgramming3D.WaypointSystem
{
	public class Waypoint : MonoBehaviour
	{
		public Vector3 Position
		{
			get { return transform.position; }
		}
	}
}
