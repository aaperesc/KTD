using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GameDefinitions;

public class UIAvailableKittyCell : MonoBehaviour {

	public Image Image;
	public Text Name;

	private KittyBehaviour Behaviour;
	private GameController GameController;

	public void Init(KittyBehaviour kittyBehaviour, GameController gameController) {
		Behaviour = kittyBehaviour;
		GameController = gameController;
		
		Name.text = Behaviour.Stats.Name;
	}

	public void OnCellClick() {
		bool success = GameController.BuyKitty(Behaviour);

		if (success) {
			print("UIAvailableKittyCell :: Bought :: " + Behaviour.Stats.Name);
		}
		else {
			print("UIAvailableKittyCell :: Not enough money");
		}
	}

}
