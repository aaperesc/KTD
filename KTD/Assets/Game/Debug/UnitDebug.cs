using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UnitDebug : MonoBehaviour {

	[Header("References")]
	public GameObject Canvas;
	public Text Text;

	[Header("Settings")]
	public bool Show = true;

	private List<DebugLine> debugLines;

	public void Clear() {
		Text.text = "";
	}

	public DebugLine AddLine(string Message) {
		DebugLine debugLine = new DebugLine(Message);
		debugLines.Add(debugLine);
		return debugLine;
	}

	private void LateUpdate() {
		Canvas.SetActive(Show);
		Clear();
		foreach (DebugLine L in debugLines) {
			Text.text += "\n" + L.Message;
		}
		Canvas.transform.LookAt(Camera.main.transform);
	}

	private void Start() {
		debugLines = new List<DebugLine>();
		Clear();
	}

}

public class DebugLine {

	public string Message;

	public DebugLine(string Message) {
		this.Message = Message;
	}

}