using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisionSort : ScriptableObject {

	public virtual GameUnit Find(GameUnit Caller, List<GameUnit> Units) {
		return new GameUnit();
	}

	public GameUnit FindByPriority(List<GameUnit> Units) {
		float maxPriority = Units.Max(x => x.Priority);
		return Units.Find(x => x.Priority == maxPriority);
	}

}
