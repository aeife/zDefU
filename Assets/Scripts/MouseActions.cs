using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseActions : MonoBehaviour {
	private GameObject[] selectedSoldiers;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

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
