using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerScriptEC))]
public class DialogueManager : MonoBehaviour {

    public event Action<sDialogueStruct> dialogueEvent = delegate { };

    public static DialogueManager instance;
    
	void Awake () {
        if (instance == null){
            instance = this;
        } else if (instance != this) {
            Destroy(this);
        }

        dialogueEvent = DM_StartDialogue;
	}
	
	// Update is called once per frame
	void Update () {
		if (true) {
			DialogueLogic ();
		}
	}

    private void DialogueLogic()
    {
        // if (dialogueTimer > cDialogueClass.fDialogueWaitTime){
        //     dialogueTimer += Time.deltaTime;
        // } else
        // {
        //     if(CheckContinueDialouge()){
        //         // DM_StartDialogue();
        //     } else {
        //         // DM_EndDialogue();
        //     }
        // }
    }

    public void ReceiveDialogue(sDialogueStruct sDialogueData){        
        if (dialogueEvent != null) {
            dialogueEvent(sDialogueData);
        }

        DM_StartDialogue(sDialogueData);
    }

    public void DM_StartDialogue(sDialogueStruct sDialogueData)
    {
        // npcTextComponent.enabled = true; 
        // EnableTextComponent(inputType);
        // if (inputType != eDialogueType.tutorial) {
        //     pickUpOrNpc = true;
        // }
    }

    public void DM_EndDialogue(eDialogueType inputType)
    {
        //StartCoroutine(EnableMovementCoroutine());
        // playCont.canMove = true;
        // inDialogue = false;
        // npcTextComponent.enabled = false;
        // pickUpOrNpc = false;
        // if (dialogueType == "tutorial") {
        //     // do gamestart things
        //     tutorialScript.ResetValues(); 
        // }
    }

    private void EnableTextComponent(eDialogueType inputType)
    {        
        // npcTextComponent.transform.parent.position = Camera.main.WorldToScreenPoint(transform.position);
        // npcTextComponent.enabled = true;
        // dialogueTimer = 0f;
        // if (inputType == eDialogueType.pickup){              
        //     SetDialogueValue(true, "pickup", false);
        //     npcTextComponent.color = Color.green;
        //     npcTextComponent.text = pickupTextToDisplay;  
        //     dialogueWaitTime = 3f;               
        //     pickUpOrNpc = true;    
        // } else if (inputType == eDialogueType.npc) {
        //     SetDialogueValue(true, "npc", false);            
        //     npcTextComponent.text = npcTextToDisplay;
        //     npcTextComponent.color = Color.blue;
        //     pickUpOrNpc = true;
        // } else if (inputType == eDialogueType.goal) {            
        //     SetDialogueValue(true, "goal");
        //     npcTextComponent.text = goalCantLeave;
        //     npcTextComponent.color = goalText.color;
        //     // replace with a variable
        //     dialogueWaitTime = 3f;
        // } else if (inputType == eDialogueType.tutorial){
        //     npcTextComponent.color = Color.blue;
        //     npcTextComponent.text = tutorialScript.ReturnCurrentTutorial();
        //     dialogueWaitTime = tutorialScript.ReturnWaitTime();
        // } else {
        //     Debug.LogError("no type specified");
        // }
    }
}
