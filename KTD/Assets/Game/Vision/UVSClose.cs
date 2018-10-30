using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Vision/Sort By Close")]
public class UVSClose : UnitVisionSort {

	public override GameUnit Find(GameUnit Caller, List<GameUnit> Units) {
		float minDistance = Units.Min(x => Vector3.Distance(Caller.transform.position, x.transform.position));
		return Units.Find(x => Vector3.Distance(Caller.transform.position, x.transform.position) == minDistance);
	}

}
