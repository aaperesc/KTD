using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDefinitions;

[CreateAssetMenu(menuName = "Game/AbilityStats")]
public class CombatAbilityStats : ScriptableObject {

	public int CurrentLevel;
	public string Name;
	public List<AbilityStatsLevel> StatsLevels;
	public UnitModifier Modifier;
	public UnitType Source;
	public UnitType[] Targets;

	public AbilityStatsLevel CurrentStatsLevel { get { return StatsLevels[CurrentLevel]; } }

}
