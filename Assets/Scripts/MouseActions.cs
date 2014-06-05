using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseActions : MonoBehaviour {
	private GameObject[] selectedSoldiers;
	private Vector2 downMousePosition;
	private bool startDragging = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			downMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			startDragging = true;
		} else if(Input.GetMouseButtonUp(0)) {
			if (startDragging) {
				Vector2 upMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				startDragging = false;
				if (downMousePosition == upMousePosition) {
					return;
				}

				float left = Mathf.Min(downMousePosition.x, upMousePosition.x);
				float top = Mathf.Min(downMousePosition.y, upMousePosition.y);
				Debug.Log("POS: " + downMousePosition.x + " : " + upMousePosition.x);
				Debug.Log("WIDTH: " + Mathf.Abs(downMousePosition.x - upMousePosition.x));
				float width = Mathf.Abs( Mathf.Min(downMousePosition.x, upMousePosition.x) - Mathf.Max(downMousePosition.x, upMousePosition.x));
				float height = Mathf.Abs( Mathf.Min(downMousePosition.y, upMousePosition.y) - Mathf.Max(downMousePosition.y, upMousePosition.y));
//				width = 0.1f;
//				height= 0.1f;
				Debug.DrawLine (new Vector3 (left, top, -5), new Vector3 (left + width, top, -5));
				Debug.DrawLine (new Vector3 (left, top, -5), new Vector3 (left, top + height, -5));
				Debug.DrawLine (new Vector3 (left, top + height, -5), new Vector3 (left + width, top + height, -5));
				Debug.DrawLine (new Vector3 (left + width, top, -5), new Vector3 (left + width, top + height, -5));
				Debug.Log("RECT: " + left + " : " + top  + " : " + width  + " : " + height);
				Rect rect = new Rect(left, top, width, height);
			
//				Rect rect = new Rect(-6.292414f, 1.207328f, 1.100068f, 1.670921f);
				GameObject[] selectedSoldiers = GameObject.FindGameObjectsWithTag ("Soldier");
				MoveToMouseXY moveScript;
				foreach (GameObject soldier in selectedSoldiers) {
					moveScript = soldier.GetComponent (typeof(MoveToMouseXY)) as MoveToMouseXY;
					if (rect.Contains(soldier.transform.position)) {
						Debug.Log("CONTAINS");
						moveScript.selected = true;
					} else {
						moveScript.selected = false;
					}
				}
			}
		}
	}

	public void toggleSoldierSelection (GameObject clickedSoldier) {
		// soldier was clicked, remove selection from all soldiers, then select clicked soldier
		selectedSoldiers = GameObject.FindGameObjectsWithTag ("Soldier");
		MoveToMouseXY moveScript;
		foreach (GameObject soldier in selectedSoldiers) {
			moveScript = soldier.GetComponent (typeof(MoveToMouseXY)) as MoveToMouseXY;
			if (moveScript.selected) {
				moveScript.selected = false;
			}
		}
		moveScript = clickedSoldier.GetComponent (typeof(MoveToMouseXY)) as MoveToMouseXY;
		moveScript.selected = true;
	}

//	public void OnMouseDown () {
//		Debug.Log ("MOUSE DOWN");
//		Rect rect = new Rect(0, 0, 150, 150);
//		GameObject[] selectedSoldiers = GameObject.FindGameObjectsWithTag ("Soldier");
//		foreach (GameObject soldier in selectedSoldiers) {
//			if (rect.Contains(soldier.transform.position))
//				Debug.Log("Inside");
//		}
//	}
}
