using UnityEngine;

using GameDefinitions;

[RequireComponent(typeof(GameUnit))]
[RequireComponent(typeof(UnitVision))]
public class UnitCombat : MonoBehaviour {

	private UnitVision Vision;
	private GameUnit GameUnit;
	private GameUnit CurrentTarget;
	private float AttackCooldown;

	private void Awake() {
		GameUnit = GetComponent<GameUnit>();
		Vision = GetComponent<UnitVision>();
	}

	private void Update() {

		if (Vision.UnitOfInterest() != null) {
			CurrentTarget = Vision.UnitOfInterest();
		} else {
			CurrentTarget = Vision.SecondaryUnitOfInterest();
		}

		if (CurrentTarget != null && GameUnit.InReach(CurrentTarget)) {
			Vector3 dir = CurrentTarget.transform.position - transform.position;
			Quaternion rot = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Slerp(transform.rotation, rot, 2.5f * Time.deltaTime);
			Attack();
		}
	}

	private void Attack() {
		if (AttackCooldown <= 0f) {
			AttackCooldown = GameUnit.GetRuntimeBehaviour.GetRuntimeStats.AttackSpeed;
			GameUnit.AnimationState = UnitAnimationState.Attacking;
			HitConfirmed();
		} else {
			AttackCooldown -= Time.deltaTime;
		}
	}

	public void HitConfirmed() {
		GameUnit.GetRuntimeBehaviour.Attack(CurrentTarget);
	}

}
