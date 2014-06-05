using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
public class MoveToMouseXY : MonoBehaviour{
	
	private PolyNavAgent _agent;
	public bool selected = false;
	public bool deselectAfterMove = false;
	private Vector3 lastMouseDownPosition;

	void start () {
	}

	public PolyNavAgent agent{
		get
		{
			if (!_agent)
				_agent = GetComponent(typeof(PolyNavAgent)) as PolyNavAgent;
			return _agent;			
		}
	}
	
	private void Update () {
		// TODO: only instanciate once
		MouseActions mouseActions = GameObject.Find ("MouseActions").GetComponent (typeof(MouseActions)) as MouseActions;

		if (Input.GetMouseButtonDown(0)) {
			// save last mouse button down to check threshold later
			lastMouseDownPosition = Input.mousePosition;
		}

		// only move if selected and mouse was not dragged further than threshold since last mouseDown
		if (selected && Input.GetMouseButtonUp(0) && (Vector3.Distance(lastMouseDownPosition, Input.mousePosition) < mouseActions.clickDragThreshold)) {
			// check if only first select click and no move click
			// TODO: handle better, distances very near
			if (Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject.transform.position) > 10.1f) {
				MoveTo (Input.mousePosition);
			}
		}
	}
	
	//Message from Agent
	private void OnDestinationReached(){
		
		//do something here...
	}

	void MoveTo (Vector3 targetPosition) {
		agent.SetDestination(Camera.main.ScreenToWorldPoint(targetPosition));
		if (deselectAfterMove) {
			setSelection(false);
		}
	}
	
	//Message from Agent
	private void OnDestinationInvalid(){
		
		//do something here...
	}

	void OnMouseDown () {
		MouseActions mouseActions = GameObject.Find ("MouseActions").GetComponent (typeof(MouseActions)) as MouseActions;
		mouseActions.toggleSoldierSelection (gameObject);
	}

	// handle change of selection
	public void setSelection (bool value) {
		selected = value;
		if (selected) {
			GetComponent<SpriteRenderer>().color = Color.gray;
		} else {
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}
}
