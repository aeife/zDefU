using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public Vector3 target;
	public float moveSpeed = 10.0f;
	CharacterController controller;
	Movement moveScript;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		moveScript = GetComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update () {
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
