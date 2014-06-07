using UnityEngine;
using System.Collections;
using Pathfinding;

public class Movement : MonoBehaviour {
	public float moveSpeed = 10.0f;
	float rotationSpeed = 50.0f;
	Path path;
	int currentWaypoint = 0;
	float nextWaypointDistance = 3;
	CharacterController controller;
	Vector3 target;
	bool moving = false;
	Seeker seeker;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		seeker = GetComponent<Seeker> ();
	}

	public void moveTo (Vector3 targetPosition) {
		moving = true;
		target = targetPosition;
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}

	void OnPathComplete (Path p) {
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}

	}
		
	void Update () {
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
		controller.Move (dir);
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}


	}

	void moveToPosition (Vector3 position) {
		Vector3 dir = (position - transform.position).normalized;
		dir *= moveSpeed * Time.fixedDeltaTime;
		controller.Move (dir);
	}
}
