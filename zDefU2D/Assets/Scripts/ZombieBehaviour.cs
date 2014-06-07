using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolyNavAgent))]
public class ZombieBehaviour : MonoBehaviour {
	private PolyNavAgent _agent;

	private GameObject[] soldiers;

	public PolyNavAgent agent{
		get
		{
			if (!_agent)
				_agent = GetComponent(typeof(PolyNavAgent)) as PolyNavAgent;
			return _agent;			
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		attack (getNearestSoldier());
			
	}

	void attack (GameObject target){
		if (target) {
			agent.SetDestination(target.transform.position);
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
