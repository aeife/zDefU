using UnityEngine;
using System.Collections;
using Pathfinding;

public class ZombieBehaviour : MonoBehaviour {
	GameObject[] soldiers;
	Movement moveScript;
	// time in seconds to look again for nearest soldier to attack
	public float refreshRate = 0.1f;
	float currentRefreshCountdown;
	Vector3 lastTargetPosition;

	// Use this for initialization
	void Start () {
		moveScript = GetComponent<Movement>();
		currentRefreshCountdown = refreshRate;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentRefreshCountdown <= 0) {
			currentRefreshCountdown = refreshRate;
			attack (getNearestSoldier ());
		} else {
			currentRefreshCountdown  -= Time.deltaTime;
		}
	}

	void attack (GameObject target){
//		if (Vector3.Distance (lastTargetPosition, target.transform.position) < 3) {
//			return;
//		}
		if (target) {
			lastTargetPosition = target.transform.position;
			moveScript.moveTo(target.transform.position);
		}
	}

	GameObject getNearestSoldier () {
		float minDistance = float.MaxValue;
		soldiers = GameObject.FindGameObjectsWithTag ("Soldier");
		GameObject nearestSoldier = null;
		foreach (GameObject soldier in soldiers) {
			float distance = Vector3.Distance(transform.position, soldier.transform.position);
			if (distance < minDistance) {
				minDistance = distance;
				nearestSoldier = soldier;
			}
		}
		
		return nearestSoldier;
	}
}
