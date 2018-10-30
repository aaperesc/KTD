using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Spin2Win : MonoBehaviour {

	public float AnimationTime;
	public float Speed;
	public CombatAbilityUnit AbilityUnit;
	public ParticleSystem[] particles;

	private bool foundOwner = false;

	private void Awake() {
		StartCoroutine(CAnimate());
		foreach (ParticleSystem ps in particles) {
			ps.Pause();
		}
	}

	private void Update() {
		if (foundOwner) {
			AbilityUnit.Owner.transform.Rotate(Vector3.up * Speed * Time.deltaTime, Space.World);
			//transform.Rotate(Vector3.up * Speed * Time.deltaTime, Space.World);
		}
	}

	private IEnumerator CAnimate() {
		while (AbilityUnit.Owner == null) {
			yield return null;
		}
		foreach (ParticleSystem ps in particles) {
			ps.Play();
		}
		foundOwner = true;
		transform.SetParent(AbilityUnit.Owner.transform);

		yield return new WaitForSeconds(AnimationTime);
		
		AbilityUnit.Kill();
	}

}
