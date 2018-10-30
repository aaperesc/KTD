using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDefinitions;

public class CombatAbilityUnit : MonoBehaviour {

	public CombatAbilityBehaviour Behaviour;
	public GameObject StartEffect;
	public GameObject HitEffect;
	public GameUnit Owner;

	private CombatAbilityBehaviour RuntimeBehaviour;
	private bool isAlive = true;
	private Vector3 destination;

	private void OnTriggerEnter(Collider collider) {
		GameUnit gameUnit = collider.gameObject.GetComponent<GameUnit>();
		if (gameUnit == null) return;
		if (gameUnit == Owner) return;

		bool hitValidTarget = false;
		foreach (UnitType ut in RuntimeBehaviour.Stats.Targets) {
			if (gameUnit.gameObject.layer == UnitTypeLayer.GetTypeLayer(ut)) {
				hitValidTarget = true;
			}
		}

		if (!hitValidTarget) return;
		
		Hit(gameUnit);
		if (Behaviour.DestroyOnHit) Kill();
	}

	private void Awake() {
		RuntimeBehaviour = Instantiate(Behaviour);

		if (StartEffect != null) StartEffect.SetActive(false);
		if (HitEffect != null) HitEffect.SetActive(false);
	}

	private void Hit(GameUnit target) {
		if (RuntimeBehaviour.Stats.Modifier != null) {
			target.Hit(RuntimeBehaviour.Stats.CurrentStatsLevel.Damage, RuntimeBehaviour.Stats.Modifier);
		}
		else {
			target.Hit(RuntimeBehaviour.Stats.CurrentStatsLevel.Damage);
		}
		RuntimeBehaviour.OnHit();
		if (HitEffect != null) {
			HitEffect.transform.SetParent(null, true);
			HitEffect.SetActive(true);
		}
	}

	public void Kill() {
		isAlive = false;
		Destroy(gameObject);
	}

	private void IsOnDestination() {
		if (Vector3.Distance (transform.position, destination) == 0f) {
			Kill();
		}
	}

	public void BeginWithMovement(Vector3 start, Vector3 target, GameUnit owner) {
		this.Owner = owner;
		if (StartEffect != null) StartEffect.SetActive(true);
		destination = target;
		transform.position = start;
		StartCoroutine(CMoveTowards(target));
		InvokeRepeating("IsOnDestination", 0f, GameStaticVariables.MovementRefreshTime);
	}

	public void Begin(GameUnit owner) {
		this.Owner = owner;
	}

	private IEnumerator CMoveTowards(Vector3 target) {
		float step = RuntimeBehaviour.Stats.CurrentStatsLevel.Velocity * Time.deltaTime;
		while (isAlive) {
			transform.LookAt(target);
			transform.position = Vector3.MoveTowards(transform.position, target, step);
			yield return null;
		}
	}

}
