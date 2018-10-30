using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Items/Item")]
public class Item : ScriptableObject {

	public string Name;
	public int Socket;
	public GameObject Object;

}