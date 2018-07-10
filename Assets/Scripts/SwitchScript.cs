using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {

    //PlayerScriptEC myPlay;
    public NotFloorScript notFloor;

    // Use this for initialization
    void Start () {
        //myPlay = FindObjectOfType<PlayerScriptEC>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //myPlay.Interact();
            notFloor.SwitchMat();
        }
    }
}
