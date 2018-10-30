using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDefinitions;

[CreateAssetMenu(menuName = "Game/KittyStats")]
public class KittyStats : UnitStats {

	public Sprite Thumbnail;
	public List<TransformSocket> sockets;

}
