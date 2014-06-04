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
			Debug.Log ("looping");
			float distance = Vector3.Distance(transform.position, soldier.transform.position);
			Debug.Log(distance);
			if (distance < minDistance) {
				Debug.Log ("got new nearest");
				minDistance = distance;
				nearestSoldier = soldier;
			}
		}
		Debug.Log (nearestSoldier);
		agent.SetDestination(Camera.main.ScreenToWorldPoint(nearestSoldier.transform.position));
	}
}
