using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Items/Equipped Items")]
public class EquippedItems : ScriptableObject {

	public List<Item> Items;
	public Material MaterialOverride;

}