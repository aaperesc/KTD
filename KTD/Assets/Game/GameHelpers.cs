using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameHelpers {

	public static Transform FindInChildren(this GameObject go, string name) {
		return (from x in go.GetComponentsInChildren<Transform>()
				where x.gameObject.name == name
				select x.gameObject).First().transform;
	}

}
