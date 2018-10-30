using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

using GameDefinitions;

public class EnemyUnit : GameUnit {
	
	private EnemyBehaviour RuntimeBehaviour;
	private NavMeshAgent NavMeshAgent;
	private Animator Animator;
	private UnitVision Vision;
	private GameUnit PrimaryTarget;
	private GameUnit SecondaryTarget;
	private GameUnit CurrentTarget;
	private bool ReachedFinalDestination;
	private float AttackCooldown;

	public static event Action<GameUnit> OnCucumberAdded = delegate { };
	public static event Action<GameUnit> OnCucumberRemoved = delegate { };

	public override UnitBehaviour GetRuntimeBehaviour { get { return RuntimeBehaviour; } }

	public void Init(EnemyBehaviour enemyBehaviour) {
		RuntimeBehaviour = Instantiate(enemyBehaviour);
		RuntimeBehaviour.onDead += OnDead;
		RuntimeBehaviour.StartBehaviour(this);

		NavMeshAgent.stoppingDistance = RuntimeBehaviour.Stats.AttackRange;
		NavMeshAgent.speed = RuntimeBehaviour.Stats.MovementSpeed;

		name = RuntimeBehaviour.Stats.Name;
	}

	public override void Hit(Damage damage) {
		RuntimeBehaviour.Hit(damage);
	}

	public override void Awake() {
		base.Awake();
		NavMeshAgent = GetComponent<NavMeshAgent>();

		if (Behaviour != null) {
			Init((EnemyBehaviour)Behaviour);
		}
	}

	private void OnEnable() {
		OnCucumberAdded((GameUnit)this);
	}

	private void OnDisable() {
		OnCucumberRemoved((GameUnit)this);
	}

	private void Start() {
		InitSockets();
	}

	private void InitSockets() {
		foreach (TransformSocket s in RuntimeBehaviour.GetRuntimeEnemyStats.sockets) {
			s.transform = GameHelpers.FindInChildren(gameObject, s.name);
			if (s.name == GameStaticVariables.HitTransformSocketName) {
				HitTarget = s.transform;
			}
		}
	}
	
	private void Update() {
		RuntimeBehaviour.UpdateBehaviour();
	}
	
	private void OnDestroy() {
		RuntimeBehaviour.onDead -= OnDead;
	}

	private void OnDead() {
        isAlive = false;
		Destroy(gameObject);
	}

}
