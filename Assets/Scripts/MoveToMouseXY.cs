using UnityEngine;
using System.Collections;

public class MoveToMouseXY : MonoBehaviour {

	public float speed = 1.5f;
	private Vector2 target;
	private bool selected = false;

	// Use this for initialization
	void Start () {
		target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && selected) {
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime);
	}

	void OnMouseDown() {
		selected = !selected;
	}
}