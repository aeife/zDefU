using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public float spawnTime = 5f;
	public float spawnDelay = 3f;
	public GameObject zombie;

	// Use this for initialization
	void Start () {
		// TODO: use for soldier shoot
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	void Spawn () {
		Instantiate (zombie, transform.position, transform.rotation);
	}
}