using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/EnemyRound")]
public class EnemyRound : ScriptableObject {

	public float EnemyDelay;
	public string Name;
	public List<EnemyBehaviour> EnemyBehaviours;

}
