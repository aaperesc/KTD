using UnityEngine;
using GameDefinitions;

public abstract class UnitStats : ScriptableObject {

	public string Name;
	public int Gold;
	public int HealthPoints;
	public float AttackSpeed;
	public float AttackRange = GameStaticVariables.MeleeAttackRange;
	public int MovementSpeed;

	public GameUnit GameUnitObject;

}
