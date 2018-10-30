using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDefinitions;

public class KittyBase : GameUnit {

	public KittyBaseStats Stats;
	public Transform BaseTargetDestination;

	private KittyBaseStats RuntimeStats;
	private float GizmosSize = 2f;

	public static event Action<GameUnit> OnBaseAdded = delegate { };
	public static event Action<GameUnit> OnBaseRemoved = delegate { };

	public override void Awake() {
		base.Awake();
		OnBaseAdded((GameUnit)this);
		RuntimeStats = Instantiate(Stats);
	}

	private void OnDestroy() {
		OnBaseRemoved((GameUnit)this);
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Vector3 position = BaseTargetDestination.position;
		position.y += GizmosSize / 2f;
		Gizmos.DrawWireCube(position, Vector3.one * GizmosSize);
	}

	public void Hit(Damage damage) {
		// IGNORE DAMAGE TYPE FOR NOW
		// IGNORE DAMAGE REDUCTIONS AND MULTIPLIERS FOR NOW
		if (!isAlive) return;

		RuntimeStats.HealthPoints -= damage.Amount;
		Debug.Log("KittyBase :: Hit :: HealthPoints :: " + RuntimeStats.HealthPoints);

		if (RuntimeStats.HealthPoints <= 0) Die();
	}

	public void Die() {
		isAlive = false;
	} 

}