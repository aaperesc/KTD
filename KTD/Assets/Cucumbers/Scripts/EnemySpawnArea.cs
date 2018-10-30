using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnArea : MonoBehaviour {

	public float Radius = 2f;

	public Vector3 RandomSpawnPosition() {
		Vector3 RSP = Random.insideUnitSphere * Radius;
		RSP += transform.position;
		RSP.y = transform.position.y;
		return RSP;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, Radius);
	}

}
