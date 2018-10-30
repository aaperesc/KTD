using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittyAnimationInterface : MonoBehaviour {

	private UnitCombat UnitCombat;

	private void Start() {
		UnitCombat = GetComponentInParent<UnitCombat>();
	}

	public void HitConfirmed() {
		UnitCombat.HitConfirmed();
	}

}
