using System;
using UnityEngine;

namespace GameProgramming3D.SaveLoad
{
	[Serializable]
	public class UnitData
	{
		public int Id;
		public int PlayerId;
		public int Health;
		public Vector3 Position;
		public Vector3 Rotation;

		public UnitData( Unit unit )
		{
			Id = unit.Id;
			PlayerId = unit.AssociatedPlayer.Id;
			Health = unit.Health;
			Position = unit.transform.position;
			Rotation = unit.transform.eulerAngles;
		}
	}
}
