using UnityEngine;
using System.Collections;

public class HealthManagement : MonoBehaviour {
	public int health = 100;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Bullet") {
			BulletScript bullet = other.gameObject.GetComponent<BulletScript>();
			takeDamage(bullet.damage);
			Destroy(bullet.gameObject);
		}
	}

	void takeDamage (int damage) {
		health -= damage;
		if (health <= 0) {
			Destroy(gameObject);
		}
	}
}
