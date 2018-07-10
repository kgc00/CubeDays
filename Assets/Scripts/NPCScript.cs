using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour {

    PlayerScriptEC myPlay;
    public float dialogueWaitTime;
    float dialogueLength;
    int dialogueType;
    public string dialogueText;

    // Use this for initialization
    void Start()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.color = new Color(Random.value, Random.value, Random.value);
        myPlay = FindObjectOfType<PlayerScriptEC>();        

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {        
        if (other.tag == "Player")
        {
            myPlay.Interact(dialogueWaitTime, dialogueText);
        }
    }
}
