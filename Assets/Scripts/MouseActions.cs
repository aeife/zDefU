using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseActions : MonoBehaviour {
	private GameObject[] selectedSoldiers;
	private Vector2 downMousePosition;

	// threshold to differentiate between single selection and selection rect
	public float clickDragThreshold = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			// save last mouseDown position
			downMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		} else if(Input.GetMouseButtonUp(0)) {
			Vector2 upMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// only do rect selection of mouse was dragged further than threshold
			if (Vector3.Distance(downMousePosition, upMousePosition) < clickDragThreshold) {
				return;
			}

			// calculate rect attributes
			float left = Mathf.Min(downMousePosition.x, upMousePosition.x);
			float top = Mathf.Min(downMousePosition.y, upMousePosition.y);
			float width = Mathf.Abs( Mathf.Min(downMousePosition.x, upMousePosition.x) - Mathf.Max(downMousePosition.x, upMousePosition.x));
			float height = Mathf.Abs( Mathf.Min(downMousePosition.y, upMousePosition.y) - Mathf.Max(downMousePosition.y, upMousePosition.y));
//			Draw Rect for Debug
//			Debug.DrawLine (new Vector3 (left, top, -5), new Vector3 (left + width, top, -5));
//			Debug.DrawLine (new Vector3 (left, top, -5), new Vector3 (left, top + height, -5));
//			Debug.DrawLine (new Vector3 (left, top + height, -5), new Vector3 (left + width, top + height, -5));
//			Debug.DrawLine (new Vector3 (left + width, top, -5), new Vector3 (left + width, top + height, -5));
//			Debug.Log("RECT: " + left + " : " + top  + " : " + width  + " : " + height);

			// generate selection rect
			Rect rect = new Rect(left, top, width, height);
		
			// fetch all soldier objects
			GameObject[] selectedSoldiers = GameObject.FindGameObjectsWithTag ("Soldier");
			MoveToMouseXY moveScript;
			foreach (GameObject soldier in selectedSoldiers) {
				moveScript = soldier.GetComponent (typeof(MoveToMouseXY)) as MoveToMouseXY;
				// select all soldiers in rect, deselect all outside
				if (rect.Contains(soldier.transform.position)) {
					Debug.Log("CONTAINS");
					moveScript.selected = true;
				} else {
					moveScript.selected = false;
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
}
