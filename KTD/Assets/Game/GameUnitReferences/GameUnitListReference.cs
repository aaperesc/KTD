using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameUnitListReference : ScriptableObject {

	public List<GameUnit> GameUnits;

	private void OnEnable() {
		GameUnits = new List<GameUnit>();
	}

}
