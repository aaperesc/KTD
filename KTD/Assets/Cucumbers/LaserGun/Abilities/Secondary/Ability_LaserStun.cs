using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_LaserStun : MonoBehaviour {

	public AnimationCurve Curve;
	public float AnimationTime;
	public float Speed;
	public float MaxRange;
	public CombatAbilityUnit AbilityUnit;

	private float CurrentRange { get { return transform.localScale.x; } }

	private void Awake() {
		StartCoroutine(CAnimate());
	}

	private IEnumerator CAnimate() {
		float elapsedTime = 0f;
		Vector3 initialRange = Vector3.one * CurrentRange;
		while (elapsedTime <= AnimationTime) {
			float t = elapsedTime / AnimationTime;
			transform.localScale = Vector3.Lerp(initialRange, MaxRange * Vector3.one, Curve.Evaluate(t));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.localScale = Vector3.one * MaxRange;
		AbilityUnit.Kill();
	}

}
