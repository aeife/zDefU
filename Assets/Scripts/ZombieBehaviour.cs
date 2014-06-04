using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolyNavAgent))]
public class ZombieBehaviour : MonoBehaviour {
	private PolyNavAgent _agent;

	private GameObject[] soldiers;
	private float minDistance = 99999;
	private GameObject nearestSoldier;

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
		attack ();
	}
	
	// Update is called once per frame
	void Update () {

			agent.SetDestination(nearestSoldier.transform.position);
	}

	void attack (){
		soldiers = GameObject.FindGameObjectsWithTag ("Soldier");
		foreach (GameObject soldier in soldiers) {
			float distance = Vector3.Distance(transform.position, soldier.transform.position);
			if (distance < minDistance) {
				minDistance = distance;
				nearestSoldier = soldier;
			}
		}
		agent.SetDestination(Camera.main.ScreenToWorldPoint(nearestSoldier.transform.position));
	}
}
