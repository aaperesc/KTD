using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GameDefinitions;

public class GameUI : MonoBehaviour {

	[Header("Current Kitty Units")]
	public UIKittyCell KittyCellPrefab;
	public Transform KittyCellContainer;

	[Header("Available Kitty Units")]
	public UIAvailableKittyCell AvailableKittyCellPrefab;
	public Transform AvailableKittyCellContainer;

	[Header("Current Enemy Units")]
	public UIEnemyCell EnemyCellPrefab;
	public Transform EnemyCellContainer;

	[Header("Panels")]
	public GameObject PreroundPanel;
	public GameObject GamePanel;
	public GameObject PausePanel;

	[Header("Gold")]
	public Text GoldAmount;

	[Header("Settings")]
	public AnimationCurve Curve;
	public float AnimationTime = 0.25f;

	[HideInInspector] public GameController GameController;
	private RectTransform KittyCellContainerRectTransform;

	public void PreroundSetup(List<EnemyBehaviour> enemies) {
		ActivatePanel(PreroundPanel);
		UpdateNextRoundEnemies(enemies);
		UpdateAvailableKittyUnits();
	}

	public void StartNextRound() {
		ActivatePanel(GamePanel);
		UpdateCurrentKittyUnits();
		StartCoroutine(CAnimateHeight(KittyCellContainerRectTransform, 300f));
		GameController.GameMode.NextRound();
	}
	
	private void Awake() {
		GameController = GetComponent<GameController>();
		KittyCellContainerRectTransform = KittyCellContainer.GetComponent<RectTransform>();

		KittyCellContainerRectTransform.sizeDelta = new Vector2(KittyCellContainerRectTransform.sizeDelta.x, 0f);
	}

	private void ActivatePanel(GameObject Panel) {
		PreroundPanel.SetActive(false);
		GamePanel.SetActive(false);
		//PausePanel.SetActive(false);

		Panel.SetActive(true);
	}

	private void UpdateCurrentKittyUnits() {
		ClearCells(KittyCellContainer);
		foreach (KittyBehaviour b in GameController.CurrentKittyUnits) {
			UIKittyCell newCell = Instantiate(KittyCellPrefab);
			newCell.Init(b, GameController);
			newCell.transform.SetParent(KittyCellContainer, false);
		}
	}

	private void UpdateAvailableKittyUnits() {
		ClearCells(AvailableKittyCellContainer);
		foreach (KittyBehaviour b in GameController.AvailableKittyUnits) {
			UIAvailableKittyCell newCell = Instantiate(AvailableKittyCellPrefab);
			newCell.Init(b, GameController);
			newCell.transform.SetParent(AvailableKittyCellContainer, false);
		}
	}

	private void UpdateNextRoundEnemies(List<EnemyBehaviour> enemies) {
		ClearCells(EnemyCellContainer);
		Dictionary<string, EnemyUnitUIInfo> types = new Dictionary<string, EnemyUnitUIInfo>();
		foreach (EnemyBehaviour b in enemies) {
			if (!types.ContainsKey(b.Stats.Name)) {
				EnemyUnitUIInfo info = new EnemyUnitUIInfo() {
					Amount = 1,
					Name = b.Stats.Name
				};
				types.Add(b.Stats.Name, info);
			} else {
				types[b.Stats.Name].Amount++;
			}
		}
		foreach (KeyValuePair<string, EnemyUnitUIInfo> kvp in types) {
			UIEnemyCell newCell = Instantiate(EnemyCellPrefab);
			newCell.Init(kvp.Value);
			newCell.transform.SetParent(EnemyCellContainer, false);
		}
	}

	private void ClearCells(Transform parent) {
		List<GameObject> children = new List<GameObject>();
		foreach (Transform child in parent) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
	}

	IEnumerator CAnimateHeight(RectTransform rectTransform, float targetHeight) {
		float elapsedTime = 0f;
		Vector2 startSize = rectTransform.sizeDelta;
		Vector2 targetSize = new Vector2(startSize.x, targetHeight);
		while (elapsedTime < AnimationTime) {
			float t = elapsedTime / AnimationTime;
			rectTransform.sizeDelta = Vector2.Lerp(startSize, targetSize, Curve.Evaluate(t));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		rectTransform.sizeDelta = targetSize;
	}

}
