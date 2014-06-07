using UnityEngine;
using System.Collections;
using Pathfinding;

public class Movement : MonoBehaviour {
	Seeker seeker;
	public float moveSpeed = 10.0f;
	float rotationSpeed = 50.0f;
	Path path;
	int currentWaypoint = 0;
	float nextWaypointDistance = 3;
	
	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
	}

	public void moveTo (Vector3 targetPosition) {
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
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
