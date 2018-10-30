using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GameDefinitions;

public class UIKittyCell : MonoBehaviour {

	public Image Thumbnail;
	public Text Name;

	private KittyBehaviour Behaviour;
	private GameController GameController;

	public void Init(KittyBehaviour kittyBehaviour, GameController gameController) {
		Behaviour = kittyBehaviour;
		GameController = gameController;

		Name.text = Behaviour.Stats.Name;
	}

	public void OnCellClick() {
		GameController.SpawnKitty(Behaviour);
		Destroy(gameObject);
	}

	public void OnCellDown() {
		GameController.PreprareSpawnKitty(Behaviour);
	}

	public void OnCellUp() {
		GameController.SpawnPreparedKitty();
		Destroy(gameObject);
	}

}
