using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDebugger : MonoBehaviour {

	List<CameraTriggerScript> triggers;
	public bool debug = false;

	// Use this for initialization
	void Start () {
		Initialize ();
	}

	void Initialize(){
		triggers = new List<CameraTriggerScript>();
		triggers.AddRange (FindObjectsOfType<CameraTriggerScript> ());
		foreach (CameraTriggerScript cts in triggers) {
			if (debug) {
				//cts.OnDrawGizmo ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
