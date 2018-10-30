using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDefinitions;

[RequireComponent(typeof(GameUnit))]
public class UnitVision : MonoBehaviour {

	public bool OverrideTargetByPriority = true;
	public float ViewDistance;

	public GameUnitListReference PrimaryTargetReference;
	public UnitVisionSort PrimarySort;

	public GameUnitListReference SecondaryTargetReference;
	public UnitVisionSort SecondarySort;

	private GameUnit GameUnit;

	private void Awake() {
		GameUnit = GetComponent<GameUnit>();
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, ViewDistance);
	}

	public GameUnit UnitOfInterest() {
		if (PrimaryTargetReference.GameUnits.Count == 0) return null;

		if (!OverrideTargetByPriority) {
			return PrimarySort.Find(GameUnit, PrimaryTargetReference.GameUnits);
		}
		else {
			return PrimarySort.FindByPriority(PrimaryTargetReference.GameUnits);
		}
	}

	public GameUnit SecondaryUnitOfInterest() {
		if (SecondaryTargetReference.GameUnits.Count == 0) return null;

		if (!OverrideTargetByPriority) {
			return SecondarySort.Find(GameUnit, SecondaryTargetReference.GameUnits);
		} else {
			return SecondarySort.FindByPriority(SecondaryTargetReference.GameUnits);
		}
	}

}
