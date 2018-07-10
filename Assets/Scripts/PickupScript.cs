using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {

    PlayerScriptEC myPlay;

    // Use this for initialization
    void Start () {
		Renderer rend = gameObject.GetComponent<Renderer> ();
		rend.material.color = new Color (Random.value, Random.value, Random.value);
        myPlay = FindObjectOfType<PlayerScriptEC>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            myPlay.hasKey = true;
            myPlay.inventoryString = myPlay.inventoryString + "KeySphere";
            myPlay.Pickup();
            Destroy(gameObject);
        }
    }
}
