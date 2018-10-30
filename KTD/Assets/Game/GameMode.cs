using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameMode")]
public class GameMode : ScriptableObject {

	public List<EnemyRound> Rounds;
	public EnemyUnit EnemyUnitPrefab;
	public int StartingGold;

	private int _currentGold;
	public int CurrentGold {
		get { return _currentGold; }
		set {
			_currentGold = value;
			GameController.GameUI.GoldAmount.text = "Gold " + _currentGold.ToString();
		}
	}
	public int CurrentRound;
	public List<EnemyBehaviour> CurrentRoundEnemies { get { return Rounds[CurrentRound].EnemyBehaviours; } }
	public Vector3 KittyBasePosition { get { return KittyBase.BaseTargetDestination.position; } }

	private GameController GameController;
	private EnemySpawnArea EnemySpawnArea;
	private KittyBase KittyBase;
	private List<EnemyBehaviour> RoundEnemies;

	public void Init(GameController game) {
		GameController = game;
		CurrentGold = StartingGold;
	}

	public void StartGame() {
		CurrentRound = 0;
		Preround();
	}

	public void NextRound() {
		StartRound();
	}

	public void Update() {
		// Count number of enemies in the battlefield

	}

	private void Awake() {
		EnemySpawnArea = FindObjectOfType<EnemySpawnArea>();
		KittyBase = FindObjectOfType<KittyBase>();
	}

	private void Preround() {
		GameController.GameUI.PreroundSetup(CurrentRoundEnemies);
	}

	private void StartRound() {
		RoundEnemies = new List<EnemyBehaviour>();
		GameController.StartCoroutine(CRound());
	}

	private void EndGame() {
		Debug.Log("Game ended");
	}

	private void OnEnemyUnitDead() {
		//behaviour.onDead -= OnEnemyUnitDead;
		//RoundEnemies.Remove(behaviour);
		if (RoundEnemies.Count == 0 && CurrentRound < Rounds.Count) {
			Preround();
		}
	}

	IEnumerator CRound() {
		foreach (EnemyBehaviour enemyBehaviour in CurrentRoundEnemies) {
			EnemyUnit newUnit = GameController.Instantiate(EnemyUnitPrefab);
			newUnit.Init(enemyBehaviour);
			newUnit.transform.position = EnemySpawnArea.RandomSpawnPosition();
			RoundEnemies.Add(newUnit.GetRuntimeBehaviour as EnemyBehaviour);
			newUnit.GetRuntimeBehaviour.onDead += OnEnemyUnitDead;
			yield return new WaitForSeconds(Rounds[CurrentRound].EnemyDelay);
		}

		CurrentRound++;
		if (CurrentRound < Rounds.Count) {
			EndGame();
		}
	}
}
