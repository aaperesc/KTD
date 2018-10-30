using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDefinitions;

public class _oldUnitVision : MonoBehaviour {

	//public bool OverrideTargetByPriority = true;
	//public float ViewDistance;
	//public UnitType TargetUnit;
	//public UnitVisionSort Sort;

	//public UnitType SecondaryTargetUnit;
	//public UnitVisionSort SecondarySort;

	//private SphereCollider Collider;
	//public List<GameUnit> Units;
	//public List<GameUnit> SecondaryUnits;
 //   private GameUnit VisionOwner;
	//private float scanDuration = 0.4f;
	//private bool scanning = false;

	//private void OnDrawGizmos() {
	//	Gizmos.color = Color.blue;
	//	Gizmos.DrawWireSphere(transform.position, ViewDistance);
	//}

	//private void Awake() {
	//	Units = new List<GameUnit>();
	//	SecondaryUnits = new List<GameUnit>();
 //       VisionOwner = GetComponentInParent<GameUnit>();
	//}

	//private void Start() {
	//	Collider = GetComponent<SphereCollider>();
	//	Collider.radius = ViewDistance;
	//	StartCoroutine(Scan());
	//}

	//private IEnumerator Scan() {
	//	while (true) {
	//		float elapsedTime = 0f;
	//		scanning = true;
	//		while (elapsedTime < scanDuration) {
	//			float t = elapsedTime / scanDuration;
	//			Collider.radius = Mathf.Lerp(0f, ViewDistance, t);
	//			elapsedTime += Time.deltaTime;
	//			yield return null;
	//		}
	//		Collider.radius = ViewDistance;
	//		scanning = false;
	//		yield return new WaitForSeconds(0.5f);
	//	}
	//}

	//private void OnTriggerEnter(Collider other) {
	//	if (other.gameObject.layer == UnitTypeLayer.GetTypeLayer(TargetUnit)) {
	//		GameUnit gameUnit = other.GetComponent<GameUnit>();
	//		if (gameUnit != null && !Units.Contains(gameUnit)) {
	//			Units.Add(gameUnit);
	//		}
	//	}
	//	else if (SecondaryTargetUnit != UnitType.None && other.gameObject.layer == UnitTypeLayer.GetTypeLayer(SecondaryTargetUnit)) {
	//		GameUnit gameUnit = other.GetComponent<GameUnit>();
	//		if (gameUnit != null && !SecondaryUnits.Contains(gameUnit)) {
	//			SecondaryUnits.Add(gameUnit);
	//		}
	//	}
	//}

	//private void OnTriggerExit(Collider other) {
	//	if (scanning) return;

	//	if (other.gameObject.layer == UnitTypeLayer.GetTypeLayer(TargetUnit)) {
	//		GameUnit gameUnit = other.GetComponent<GameUnit>();
	//		if (gameUnit != null) {
	//			Units.Remove(gameUnit);
	//		}
	//	} else if (SecondaryTargetUnit != UnitType.None && other.gameObject.layer == UnitTypeLayer.GetTypeLayer(SecondaryTargetUnit)) {
	//		GameUnit gameUnit = other.GetComponent<GameUnit>();
	//		if (gameUnit != null) {
	//			SecondaryUnits.Remove(gameUnit);
	//		}
	//	}
	//}

	//public GameUnit UnitOfInterest() {
 //       if(!VisionOwner.isAlive) return null;

	//	for (int i=Units.Count-1; i>=0; i--) { 
 //           if(Units[i] == null) Units.Remove(Units[i]);
 //       }
 //       if(Units.Count == 0) return null;

	//	Units = Sort.OrderBy(VisionOwner, Units);
	//	if (OverrideTargetByPriority) Units = Sort.OrderByPriority(Units);

	//	return Units[0];
	//}

	//public GameUnit SecondaryUnitOfInterest() {
	//	if (!VisionOwner.isAlive) return null;

	//	for (int i = SecondaryUnits.Count-1; i>=0; i--) {
	//		if (SecondaryUnits[i] == null) SecondaryUnits.Remove(SecondaryUnits[i]);
	//	}
	//	if (SecondaryUnits.Count == 0) return null;

	//	SecondaryUnits = SecondarySort.OrderBy(VisionOwner, SecondaryUnits);
	//	if (OverrideTargetByPriority) SecondaryUnits = SecondarySort.OrderByPriority(SecondaryUnits);

	//	return SecondaryUnits[0];
	//}

}
