using System;

using UnityEngine;

using GameDefinitions;

public class GameUnit : MonoBehaviour {

	[Header("Settings")]
	public UnitBehaviour Behaviour;
	public virtual UnitBehaviour GetRuntimeBehaviour { get; }
	public int Priority = 0;
	public UnitAnimationState AnimationState;
	public bool isAlive = true;
	public bool isStunned = false;
	public int HealthPoints;

	public virtual void Hit(Damage damage) { }
	public virtual void Hit(Damage damage, UnitModifier modifier) { }

	[HideInInspector] public Transform HitTarget;
	[HideInInspector] public Transform AttackSource;
	[HideInInspector] public UnitDebug UnitDebug;

	public virtual void Awake() {
		UnitDebug = GetComponentInChildren<UnitDebug>();
		HitTarget = transform;
		AttackSource = transform;
	}

	public bool InReach(GameUnit Target) {
		return Vector3.Distance(transform.position, Target.transform.position) < Behaviour.Stats.AttackRange;
	}

}
