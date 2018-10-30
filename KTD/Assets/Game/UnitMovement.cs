using UnityEngine;
using UnityEngine.AI;

using GameDefinitions;

[RequireComponent(typeof(GameUnit))]
[RequireComponent(typeof(UnitVision))]
[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour {

	private NavMeshAgent NavMeshAgent;
	private UnitVision Vision;
	private bool ReachedFinalDestination;
	private GameUnit GameUnit;
	private GameUnit CurrentTarget;

	private void Awake() {
		GameUnit = GetComponent<GameUnit>();
		Vision = GetComponent<UnitVision>();
		NavMeshAgent = GetComponent<NavMeshAgent>();
		InvokeRepeating("Movement", 0f, GameStaticVariables.MovementRefreshTime);
	}

	private void Movement() {
		
		if (Vision.UnitOfInterest() != null) {
			CurrentTarget = Vision.UnitOfInterest();
		} else {
			CurrentTarget = Vision.SecondaryUnitOfInterest();
		}

		// DEBUG
		/*
		try {
			float DistanceToTarget = Vector3.Distance(transform.position, CurrentTarget.transform.position);
			string DistanceToTargetMessage = "> Distance to target: " + DistanceToTarget;
			if (DistanceDebugLine == null) DistanceDebugLine = UnitDebug.AddLine(DistanceToTargetMessage);
			DistanceDebugLine.Message = DistanceToTargetMessage;

			string TargetNameMessage = "> Target: " + CurrentTarget.name;
			if (TargetDebugLine == null) TargetDebugLine = UnitDebug.AddLine(TargetNameMessage);
			TargetDebugLine.Message = TargetNameMessage;

			string StunnedMessage = "> Stunned: " + isStunned;
			if (StunnedDebugLine == null) StunnedDebugLine = UnitDebug.AddLine(StunnedMessage);
			StunnedDebugLine.Message = StunnedMessage;
		} catch (System.NullReferenceException e) {

		}
		*/
		// 

		if (CurrentTarget != null) {
			GameUnit.AnimationState = UnitAnimationState.Running;
			SetDestination(CurrentTarget.transform.position);
		} else {
			GameUnit.AnimationState = UnitAnimationState.Idle;
		}
	}

	private void SetDestination(Vector3 destination) {
		NavMeshAgent.destination = destination;
	}

}
