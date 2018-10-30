using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Game/AbilityBehaviour")]
public class CombatAbilityBehaviour : ScriptableObject {

	public CombatAbilityStats Stats;
	public bool HasUnit;
	public bool DestroyOnHit;
	public CombatAbilityUnit AbilityPrefab;

	private CombatAbilityStats RuntimeStats;

	private void Awake() {
		RuntimeStats = Instantiate(Stats);
	}

	virtual public void OnHit() {

	}

}
