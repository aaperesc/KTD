using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDefinitions;

[CreateAssetMenu(menuName = "Game/EnemyBehaviour")]
public class EnemyBehaviour : UnitBehaviour {
	
	public CombatAbilityBehaviour BasicCombatAbility;
	public CombatAbilityBehaviour SpecialCombatAbility;

	[Header("Debug")]
	[SerializeField] private EnemyStats RuntimeStats;

	public override UnitStats GetRuntimeStats { get { return RuntimeStats; } }
	public EnemyStats GetRuntimeEnemyStats { get { return RuntimeStats; } }

	public delegate void OnDead();
	public OnDead onDead;

	private GameUnit behaviourOwner;
	private float specialAbilityCooldown;
	private bool hasSpecialAbility { get { return SpecialCombatAbility != null; } }

	virtual public void Awake() {
		RuntimeStats = Instantiate(Stats) as EnemyStats;
		if (hasSpecialAbility) RestartSpecialAbilityCooldown();
	}

	private bool CanDoSpecialAbility() {
		return specialAbilityCooldown <= 0f && hasSpecialAbility;
	}

	private void RestartSpecialAbilityCooldown() {
		specialAbilityCooldown = SpecialCombatAbility.Stats.CurrentStatsLevel.Cooldown;
	}

	private void UpdateSpecialAbilityCooldown() {
		if (hasSpecialAbility) {
			specialAbilityCooldown -= Time.deltaTime;
		}
	}

	virtual public void UpdateBehaviour() {
		UpdateSpecialAbilityCooldown();
	}

	virtual public void StartBehaviour(GameUnit owner) {
		behaviourOwner = owner;
		behaviourOwner.HealthPoints = RuntimeStats.HealthPoints;
	}

	public override void Attack(GameUnit targetUnit) {
		if (CanDoSpecialAbility()) {
			if (SpecialCombatAbility.HasUnit) {
				CombatAbilityUnit abilityUnit = Instantiate(SpecialCombatAbility.AbilityPrefab);
				abilityUnit.Begin(behaviourOwner);
				//abilityUnit.BeginWithMovement(behaviourOwner.AttackSource.transform.position, targetUnit.HitTarget.position, behaviourOwner);
			}
			RestartSpecialAbilityCooldown();
		} else {
			if (!BasicCombatAbility.HasUnit) {
				targetUnit.Hit(BasicCombatAbility.Stats.CurrentStatsLevel.Damage);
			} 
			else {
				CombatAbilityUnit abilityUnit = Instantiate(BasicCombatAbility.AbilityPrefab);
				abilityUnit.BeginWithMovement(behaviourOwner.AttackSource.transform.position, targetUnit.HitTarget.position, behaviourOwner);
			}
		}
	}

	virtual public void Hit(Damage damage) {
		// IGNORE DAMAGE TYPE FOR NOW
		// IGNORE DAMAGE REDUCTIONS AND MULTIPLIERS FOR NOW
		if (!behaviourOwner.isAlive) return;

		RuntimeStats.HealthPoints -= damage.Amount;
		behaviourOwner.HealthPoints = RuntimeStats.HealthPoints;

		if (RuntimeStats.HealthPoints <= 0) Die();
	}

	virtual public void Die() {
		behaviourOwner.isAlive = false;
		onDead();
	}

}
