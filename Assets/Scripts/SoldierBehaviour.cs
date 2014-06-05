using UnityEngine;
using System.Collections;

public class SoldierBehaviour : MonoBehaviour {
	public float fireRate = 1.5f;
	public GameObject bullet;
	public float bulletSpeed = 1f;

	private float cooldown = 0;
	private GameObject[] zombies;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;
		if (canShoot()) {
			attack(getNearestVisibleZombie ());
			cooldown = fireRate;
		}
	}

	bool canShoot () {
		return (cooldown <= 0);
	}

	GameObject getNearestVisibleZombie () {
//		Debug.Log("get NEAREST");
		float minDistance = 9999;
		zombies = GameObject.FindGameObjectsWithTag ("Zombie");
//		Debug.Log(zombies.Length);
		GameObject nearestZombie = null;
		foreach (GameObject zombie in zombies) {
			float distance = Vector3.Distance(transform.position, zombie.transform.position);
			RaycastHit2D[] hits = null;
			hits =	Physics2D.LinecastAll(transform.position, zombie.transform.position);

			if (distance < minDistance && hits.Length <= 3) {
//				Debug.Log("SETTING");
				minDistance = distance;
				nearestZombie = zombie;
			}
		}

		return nearestZombie;
	}

	void attack (GameObject nearestZombie) {
		if (nearestZombie != null) {
			GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
			Vector3 dir = nearestZombie.transform.position - newBullet.transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90;
			newBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			newBullet.rigidbody2D.AddForce(newBullet.transform.up * bulletSpeed);
		}

	}
}
