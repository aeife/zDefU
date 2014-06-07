using UnityEngine;
using System.Collections;
using Pathfinding;

public class MouseMoveToXY : MonoBehaviour {
	Movement moveScript;

	// Use this for initialization
	void Start () {
		moveScript = GetComponent<Movement>();
	}
	
	void FixedUpdate () {
		// set new target
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {         
				Debug.DrawLine (ray.origin, hit.point, Color.red, 2);
				//				Debug.Log ("Raycast succesful");
				//				Vector3 direction = hit.point;
				moveScript.moveTo(hit.point);
			}
		}
	}
}


