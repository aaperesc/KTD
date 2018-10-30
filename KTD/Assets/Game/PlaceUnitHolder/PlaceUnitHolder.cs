using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceUnitHolder : MonoBehaviour {

	[Header("Settings")]
	public Color ValidAlbedo;
	public Color InvalidAlbedo;
	public Vector3 Offset;

	[Header("References")]
	public Renderer ContainerRenderer;

	bool IsValidPosition = true;
	Material ContainerMaterial;

	private void Awake() {
		ContainerMaterial = ContainerRenderer.material;
	}

	public void SetPosition(Vector3 position) {
		transform.position = position;
	}

	public void SetIsValidPosition(bool valid) {
		IsValidPosition = valid;

		ContainerMaterial.color = IsValidPosition ? ValidAlbedo : InvalidAlbedo;
	}

}
