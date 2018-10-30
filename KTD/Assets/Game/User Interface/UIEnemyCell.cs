using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GameDefinitions;

public class UIEnemyCell : MonoBehaviour {

	public Image Thumbnail;
	public Text Name;
	public Text Amount;

	public void Init(EnemyUnitUIInfo info) {
		Thumbnail.sprite = info.Sprite;
		Name.text = info.Name;
		Amount.text = "X " + info.Amount.ToString();
	}

}
