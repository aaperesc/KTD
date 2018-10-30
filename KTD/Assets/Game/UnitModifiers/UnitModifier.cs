using UnityEngine;

using GameDefinitions;

[CreateAssetMenu(menuName = "Game/UnitModifier")]
public class UnitModifier : ScriptableObject {

	public UnitModifierType Type;
	public float Duration;
	public float Amount;

	public delegate void OnEnd(UnitModifier mod);
	public OnEnd OnEndCallback;

	private float RemainingTime;

	public void Init() {
		RemainingTime = Duration;
	}

	public void UpdateModifier(float deltaTime) {
		RemainingTime -= deltaTime;
		if (RemainingTime <= 0f) {
			OnEndCallback(this);
		}
	}

}
