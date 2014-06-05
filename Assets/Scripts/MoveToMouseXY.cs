using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
public class MoveToMouseXY : MonoBehaviour{
	
	private PolyNavAgent _agent;
	public bool selected = false;
	
	public PolyNavAgent agent{
		get
		{
			if (!_agent)
				_agent = GetComponent(typeof(PolyNavAgent)) as PolyNavAgent;
			return _agent;			
		}
	}
	
	private void Update () {
		// only move if selected
		if (selected && Input.GetMouseButtonDown(0))
			agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
