using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDefinitions;

[CreateAssetMenu(menuName = "Game/KittyBehaviour")]
public class KittyBehaviour : UnitBehaviour {

	[Header("Settings")]
	public EquippedItems EquippedItems;
	public CombatAbilityBehaviour BasicCombatAbility;
	public CombatAbilityBehaviour SpecialCombatAbility;

	[Header("Debug")]
	[SerializeField] private List<UnitModifier> Modifiers;
	[SerializeField] private KittyStats RuntimeStats;

	public override UnitStats GetRuntimeStats { get { return RuntimeStats; } }
	public KittyStats GetRuntimeKittyStats { get { return RuntimeStats; } }

	private GameUnit behaviourOwner;
	private float specialAbilityCooldown;
	private bool hasSpecialAbility { get { return SpecialCombatAbility != null; } }

	private void Awake() {
		RuntimeStats = Instantiate(Stats) as KittyStats;
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

	virtual public void UpdateBehaviour(float deltaTime) {
		for (int i=0; i<Modifiers.Count; i++) {
			Modifiers[i].UpdateModifier(deltaTime);
		}
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
				abilityUnit.BeginWithMovement(behaviourOwner.AttackSource.transform.position, targetUnit.HitTarget.position, behaviourOwner);
			}
			RestartSpecialAbilityCooldown();
		}
		else {
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
		// IGNORE DAMAGE REDUCTIONS AND MULTIPLIERS FOR NOW
		if (!behaviourOwner.isAlive) return;

		if (damage.Type == DamageType.Heal) {
			RuntimeStats.HealthPoints += damage.Amount;
		}
		else {
			RuntimeStats.HealthPoints -= damage.Amount;
		}
		behaviourOwner.HealthPoints = RuntimeStats.HealthPoints;

		if (RuntimeStats.HealthPoints <= 0) Die();
	}

	virtual public void Hit(Damage damage, UnitModifier modifier) {
		Hit(damage);
		AddModifier(modifier);
	}

	virtual public void AddModifier(UnitModifier modifier) {
		modifier.Init();
		if (!Modifiers.Contains(modifier)) {
			Modifiers.Add(modifier);
			ApplyModifier(modifier);
			modifier.OnEndCallback += RemoveModifier;
		}
	}

	virtual public void RemoveModifier(UnitModifier modifier) {
		modifier.OnEndCallback -= RemoveModifier;
		UnapplyModifier(modifier);
		Modifiers.Remove(modifier);
	}

	virtual public void ApplyModifier(UnitModifier modifier) {
		switch (modifier.Type) {
			case UnitModifierType.AttackSpeedModifier: {
				RuntimeStats.AttackSpeed = Stats.AttackSpeed * modifier.Amount;
				break;
			}
			case UnitModifierType.StunModifier: {
				behaviourOwner.isStunned = true;
				break;
			}
			default: {
				break;
			}
		}
	}

	virtual public void UnapplyModifier(UnitModifier modifier) {
		switch (modifier.Type) {
			case UnitModifierType.AttackSpeedModifier: {
				RuntimeStats.AttackSpeed = Stats.AttackSpeed / modifier.Amount;
				break;
			}
			case UnitModifierType.StunModifier: {
				behaviourOwner.isStunned = false;
				break;
			}
			default: {
				break;
			}
		}
	}

	virtual public void Die() {
		behaviourOwner.isAlive = false;
		onDead();
	}

}
