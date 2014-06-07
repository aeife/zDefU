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
		Debug.Log(gameObject.collider2D.bounds.min.x);

		Instantiate (zombie, new Vector3(Random.Range(gameObject.collider2D.bounds.min.x, gameObject.collider2D.bounds.max.x), Random.Range(gameObject.collider2D.bounds.min.y, gameObject.collider2D.bounds.max.y), gameObject.transform.position.z), transform.rotation);
	}
}