using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameProgramming3D.GUI
{

	public class TotalHealths : MonoBehaviour
	{
		[SerializeField] private HealthBar _healthBarPrefab;
		[SerializeField] private float _healthbarHeight;

		private Dictionary<Player, HealthBar> _healthBars =
			new Dictionary<Player, HealthBar> ();

		public void Init( Player[] players )
		{
			int playerIndex = 0;
			foreach ( var player in players )
			{
				var healthbar = Instantiate( _healthBarPrefab );
				healthbar.transform.SetParent( transform );
				healthbar.transform.localPosition = Vector3.zero;
				var position = healthbar.transform.localPosition;
				position.y += _healthbarHeight * playerIndex;
				healthbar.transform.localPosition = position;
				healthbar.transform.localScale = Vector3.one;

				int totalHealth = player.Units.Sum( unit => unit.Health );

				healthbar.Init( totalHealth );

				foreach ( var unit in player.Units )
				{
					unit.DamageTaken += DamageTaken;
				}

				_healthBars.Add ( player, healthbar );
				playerIndex++;
			}
		}

		private void DamageTaken( Unit sender, int obj )
		{
			var totalHealth = sender.AssociatedPlayer.Units.Sum ( unit => unit.Health );
			_healthBars[sender.AssociatedPlayer].SetHealth ( totalHealth );
		}
	}
}
