using System;
using System.Collections.Generic;

namespace GameProgramming3D.SaveLoad
{
	[Serializable]
	public class GameData
	{
		// Whos turn is it?
		public int ActivePlayer;

		// How much time we have left?
		public float CurrentTime;

		// Key: Player id, Value: Id of unit which has turn
		public Dictionary<int, int> Turns = new Dictionary< int, int >();

		// Contains datas for every unit in the game
		public List<UnitData> UnitDatas = new List< UnitData >();  
	}
}
