using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Vision/Sort By Lowest Health Points")]
public class UVSLowHP : UnitVisionSort {

	public override GameUnit Find(GameUnit Caller, List<GameUnit> Units) {
		GameUnit currentLowest = Units.First();
		foreach (GameUnit u in Units) {
			if (u.HealthPoints < currentLowest.HealthPoints) {
				currentLowest = u;
			}
		}
		if (currentLowest == Caller) return null;
		return currentLowest;
	}

}
