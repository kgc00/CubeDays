using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : DialogueObject {

    void Start()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.color = new Color(Random.value, Random.value, Random.value);    
    }

    void OnTriggerEnter(Collider other)
    {        
        if (other.tag == "Player")
        {           
            InitiateDialogue(sDialogueData);
        }
    }
}
