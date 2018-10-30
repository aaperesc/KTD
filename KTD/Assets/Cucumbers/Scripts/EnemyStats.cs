using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDefinitions;

[CreateAssetMenu(menuName = "Game/EnemyStats")]
public class EnemyStats : UnitStats {
	
	public Sprite Thumbnail;

	public EnemyUnit EnemyUnitObject;
	public List<TransformSocket> sockets;

}
