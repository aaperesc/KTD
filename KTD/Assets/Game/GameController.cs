using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static bool GameRunning = true;

	[Header("References")]
	public GameMode GameMode;
	public GameUnitListReference CucumbersReference;
	public GameUnitListReference KittiesReference;
	public GameUnitListReference BaseReference;

	[Header("Settings")]
	public LayerMask RaycastMask;
	public LayerMask ValidMask;

	[Header("Debug")]
	public List<KittyBehaviour> AvailableKittyUnits;
	public bool AutoNextRound;
	public PlaceUnitHolder unitHolder;
	public GameUI GameUI;
	public List<KittyBehaviour> CurrentKittyUnits;

	private bool RaycastValidPosition;
	private KittyUnit PreparedKitty;
	private KittyBehaviour PreparedKittyBehaviour;

	public void SpawnKitty(KittyBehaviour kittyBehaviour) {
		KittyUnit newUnit = GameController.Instantiate(kittyBehaviour.Stats.GameUnitObject) as KittyUnit;
		newUnit.Init(kittyBehaviour);
		newUnit.transform.position = Vector3.zero;
	}

	public void PreprareSpawnKitty(KittyBehaviour kittyBehaviour) {
		RaycastValidPosition = true;
		PreparedKittyBehaviour = kittyBehaviour;

		PreparedKitty = GameController.Instantiate(kittyBehaviour.Stats.GameUnitObject) as KittyUnit;
		PreparedKitty.transform.SetParent(unitHolder.transform, true);
		PreparedKitty.transform.localPosition = unitHolder.Offset;
	}

	public void SpawnPreparedKitty() {
		PreparedKitty.Init(PreparedKittyBehaviour);
		PreparedKitty.transform.SetParent(null);
		RaycastValidPosition = false;
	}

	public void CancelSpawnKitty() {
		RaycastValidPosition = false;
	}

	public bool BuyKitty(KittyBehaviour kittyBehaviour) {
		if (GameMode.CurrentGold - kittyBehaviour.Stats.Gold >= 0) {
			CurrentKittyUnits.Add(kittyBehaviour);
			GameMode.CurrentGold -= kittyBehaviour.Stats.Gold;
			return true;
		}
		return false;
	}

	private void OnKittyAdded(GameUnit unit) {
		if (!KittiesReference.GameUnits.Contains(unit)) KittiesReference.GameUnits.Add(unit);
	}

	private void OnKittyRemoved(GameUnit unit) {
		if (KittiesReference.GameUnits.Contains(unit)) KittiesReference.GameUnits.Remove(unit);
	}

	private void OnCucumberAdded(GameUnit unit) {
		if (!CucumbersReference.GameUnits.Contains(unit)) CucumbersReference.GameUnits.Add(unit);
	}

	private void OnCucumberRemoved(GameUnit unit) {
		if (CucumbersReference.GameUnits.Contains(unit)) CucumbersReference.GameUnits.Remove(unit);
	}

	private void OnBaseAdded(GameUnit unit) {
		if (!BaseReference.GameUnits.Contains(unit)) BaseReference.GameUnits.Add(unit);
	}

	private void OnBaseRemoved(GameUnit unit) {
		if (BaseReference.GameUnits.Contains(unit)) BaseReference.GameUnits.Remove(unit);
	}

	private void Awake() {
		GameMode = Instantiate(GameMode);
		GameUI = GetComponent<GameUI>();

		EnemyUnit.OnCucumberAdded += OnCucumberAdded;
		EnemyUnit.OnCucumberRemoved += OnCucumberRemoved;
		KittyUnit.OnKittyAdded += OnKittyAdded;
		KittyUnit.OnKittyRemoved += OnKittyRemoved;
		KittyBase.OnBaseAdded += OnBaseAdded;
		KittyBase.OnBaseRemoved += OnBaseRemoved;
	}

	private void OnEnable() {
		
	}

	private void OnDestroy() {
		EnemyUnit.OnCucumberAdded -= OnCucumberAdded;
		EnemyUnit.OnCucumberRemoved -= OnCucumberRemoved;
		KittyUnit.OnKittyAdded -= OnKittyAdded;
		KittyUnit.OnKittyRemoved -= OnKittyRemoved;
		KittyBase.OnBaseAdded -= OnBaseAdded;
		KittyBase.OnBaseRemoved -= OnBaseRemoved;
	}

	private void Start() {
		GameMode.Init(this);
		//GameMode.StartGame();

		Application.targetFrameRate = 60;
	}

	private void Update() {
		GameMode.Update();

		unitHolder.gameObject.SetActive(RaycastValidPosition);

		if (RaycastValidPosition) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100f, RaycastMask.value)) {
				Vector3 unitHolderPosition = hit.point;
				unitHolder.SetPosition(unitHolderPosition);				
				unitHolder.SetIsValidPosition(true);
			}
		}
	}

}
