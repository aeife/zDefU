using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
public class MoveToMouseXY : MonoBehaviour{
	
	private PolyNavAgent _agent;
	public bool selected = false;
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
			agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}
	
	//Message from Agent
	private void OnDestinationReached(){
		
		//do something here...
	}
	
	//Message from Agent
	private void OnDestinationInvalid(){
		
		//do something here...
	}

	void OnMouseDown () {
		MouseActions mouseActions = GameObject.Find ("MouseActions").GetComponent (typeof(MouseActions)) as MouseActions;
		mouseActions.toggleSoldierSelection (gameObject);
	}
}
