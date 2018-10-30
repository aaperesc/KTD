using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

using GameDefinitions;

public class KittyUnit : GameUnit {

	private KittyBehaviour RuntimeBehaviour;	
	private NavMeshAgent NavMeshAgent;

	// Debug
	private DebugLine DistanceDebugLine;
	private DebugLine TargetDebugLine;
	private DebugLine StunnedDebugLine;

	public static event Action<GameUnit> OnKittyAdded = delegate { };
	public static event Action<GameUnit> OnKittyRemoved = delegate { };

	public override UnitBehaviour GetRuntimeBehaviour { get { return RuntimeBehaviour; } }

	public void Init(KittyBehaviour kittyBehaviour) {
		RuntimeBehaviour = Instantiate(kittyBehaviour);
		RuntimeBehaviour.onDead += OnDead;
		RuntimeBehaviour.StartBehaviour(this);

		NavMeshAgent.stoppingDistance = RuntimeBehaviour.Stats.AttackRange * 0.8f;
		NavMeshAgent.speed = RuntimeBehaviour.Stats.MovementSpeed;

		name = RuntimeBehaviour.Stats.Name;
	}

	public override void Hit(Damage damage) {
		RuntimeBehaviour.Hit(damage);
	}

	public override void Hit(Damage damage, UnitModifier modifier) {
		RuntimeBehaviour.Hit(damage, modifier);
	}

	public override void Awake() {
		base.Awake();
		NavMeshAgent = GetComponent<NavMeshAgent>();

		if (Behaviour != null) {
			Init((KittyBehaviour)Behaviour);
		}
	}

	private void OnEnable() {
		OnKittyAdded(this);
	}

	private void OnDisable() {
		OnKittyRemoved(this);
	}

	private void Start() {
		InitSockets();
		EquipItems();
		OverrideMaterial();
	}

	private void InitSockets() {
		foreach (TransformSocket s in RuntimeBehaviour.GetRuntimeKittyStats.sockets) {
			s.transform = GameHelpers.FindInChildren(gameObject, s.name);
			if (s.name == GameStaticVariables.HitTransformSocketName) {
				HitTarget = s.transform;
			}
			if (s.isAttackSocket) {
				AttackSource = s.transform;
			}
		}
	}

	private void EquipItems() {
		if (RuntimeBehaviour.EquippedItems == null) return;

		foreach (Item item in RuntimeBehaviour.EquippedItems.Items) {
			if (RuntimeBehaviour.GetRuntimeKittyStats.sockets[item.Socket] != null) {
				GameObject itemObject = Instantiate(item.Object);
				itemObject.transform.SetParent(RuntimeBehaviour.GetRuntimeKittyStats.sockets[item.Socket].transform, false);
			}
		}
	}

	private void OverrideMaterial() {
		if (RuntimeBehaviour.EquippedItems == null) return;
		if (RuntimeBehaviour.EquippedItems.MaterialOverride == null) return;
		Renderer[] renderers = GetComponentsInChildren<Renderer>();

		foreach (Renderer r in renderers) {
			r.material = RuntimeBehaviour.EquippedItems.MaterialOverride;
		}
	}

	private void Update() {
		RuntimeBehaviour.UpdateBehaviour(Time.deltaTime);
	}

	private void OnDestroy() {
		RuntimeBehaviour.onDead -= OnDead;
	}

	private void OnDead() {
        isAlive = false;
		Destroy(gameObject);
	}

}
