using System;
using UnityEngine;

namespace GameProgramming3D.SaveLoad
{
	[Serializable]
	public class UnitData
	{
		// Id of the unit
		public int Id;
		// Id of the player which owns the unit
		public int PlayerId;
		// Health of the unit
		public int Health;
		// Position of the unit
		public SerializableVector3 Position;
		// Rotation of the unit
		public SerializableVector3 Rotation;

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
