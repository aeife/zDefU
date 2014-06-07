using UnityEngine;
using System.Collections;
using Pathfinding;

public class MouseMoveToXY : MonoBehaviour {
	Seeker seeker;
	public float moveSpeed = 100.0f;
	float rotationSpeed = 50.0f;
	Path path;
	int currentWaypoint = 0;
	float nextWaypointDistance = 3;
	CharacterController controller;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
	}

	public void OnPathComplete (Path p) {
		Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}
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
				seeker.StartPath (transform.position, hit.point, OnPathComplete);
			}
			
		}

		if (path == null) {
			//We have no path to move after yet
			return;
		}
		
		if (currentWaypoint >= path.vectorPath.Count) {
//			Debug.Log ("End Of Path Reached");
			return;
		}
		
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= moveSpeed * Time.fixedDeltaTime;
		// move to direction
		rigidbody.MovePosition(rigidbody.position + dir);
		
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
}


