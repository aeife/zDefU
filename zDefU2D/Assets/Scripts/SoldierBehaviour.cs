﻿using UnityEngine;
using System.Collections;

public class SoldierBehaviour : MonoBehaviour {
	public float fireRate = 1.5f;
	public GameObject bullet;
	public float bulletSpeed = 1f;
//	public int bulletDamage = 10;

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
		// check radius for objects, then check if nearest visible
		GameObject nearestZombie = null;
		// max radius: 10 * 0.5
		for (int i = 1; i <= 10; i++) {
			float minDistance = float.MaxValue;
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, i*0.5f);
			foreach (Collider2D hitCollider in hitColliders) {
				if (hitCollider.tag == "Zombie") {
					float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
					RaycastHit2D[] hits = null;
					hits =	Physics2D.LinecastAll(transform.position, hitCollider.transform.position);
					if (distance < minDistance && hits.Length <= 3) {
						minDistance = distance;
						nearestZombie = hitCollider.gameObject;
					}
				}

			}
		}
		return nearestZombie;
	}

	// if nearest zombie is available shoot in his direction
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
