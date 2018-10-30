using UnityEngine;

namespace GameDefinitions {

	public enum DamageType {
		Normal,
		Heal,
		Water,
		Fire
	}

	public enum UnitType {
		None,
		Kitty,
		Enemy,
		Base
	}

	public enum UnitModifierType {
		AttackSpeedModifier,
		MovementSpeedModifier,
		DamageOutputModifier,
		StunModifier
	}

	public static class GameStaticVariables {
		public static float MeleeAttackRange = 1.5f;
		public static float MinRangedAttackRange = 4.0f;
		public static float MovementRefreshTime = 0.16f;
		public static string HitTransformSocketName = "Back";
	}

	public static class UnitTypeLayer {
		public static int GetTypeLayer(UnitType type) {
			int layer;
			switch (type) {
				case UnitType.Enemy:
					return 10;
				case UnitType.Kitty:
					return 9;
				case UnitType.Base:
					return 16;
				default:
					return 0;
			}
		}
	}

	public enum UnitAnimationState {
		Idle,
		Running,
		Attacking
	}

	[System.Serializable]
	public class Damage {
		public DamageType Type;
		public int Amount;
	}

	[System.Serializable]
	public class AbilityStatsLevel {
		public Damage Damage;
		public float Velocity;
		public float Cooldown;
	}

	[System.Serializable]
	public class EnemyUnitUIInfo {
		public Sprite Sprite;
		public int Amount;
		public string Name;
	}

	[System.Serializable]
	public class TransformSocket {
		public string name;
		public Transform transform;
		public bool isAttackSocket = false;
	}

}
