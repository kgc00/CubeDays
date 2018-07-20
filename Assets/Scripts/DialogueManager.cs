using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using PlayerContollerEC;
// using GameStart;

// [RequireComponent(typeof(PlayerContollerEC))]
public class DialogueManager : MonoBehaviour {

	PlayerContollerEC playCont;
    PlayerScriptEC playScript;
	bool inDialogue;
	Text npcTextComponent;
	string npcTextToDisplay;
	string pickupTextToDisplay;
	Text goalText;	
	bool pickUpOrNpc;
	GameStart tutorialScript;
	float dialogueTimer;
	string goalCantLeave;
    public enum eDialogueType {tutorial, npc, goal, pickup} 

    [System.Serializable]
    public class DialogueClass {
        public eDialogueType type;
        public float fDialogueWaitTime;
        public string tTextToDisplay;
        public Text tTextComponent;
    }

    DialogueClass cDialogueClass;

	// Use this for initialization
	void Start () {
		playCont = FindObjectOfType<PlayerContollerEC>();
		tutorialScript = FindObjectOfType<GameStart>();
        playScript = FindObjectOfType<PlayerScriptEC>();
	}
	
	// Update is called once per frame
	void Update () {
		if (inDialogue) {
			DialogueLogic ();
		}
	}

    private void DialogueLogic()
    {
        if (dialogueTimer > cDialogueClass.fDialogueWaitTime){
            dialogueTimer += Time.deltaTime;
        } else
        {
            if(CheckContinueDialouge()){
                // continue dialogue
                DM_StartDialogue();
            } else {
                // end dialogue
                DM_EndDialogue();
            }
        }
    }

    private static bool CheckContinueDialouge()
    {
        // check to see if there is more dialogue after this
        Debug.Log("bl");
        bool temp = false;
        return temp;
    }

    public void DM_StartDialogue(eDialogueType inputType)
    {
        playCont.canMove = false;
        inDialogue = true;
        npcTextComponent.enabled = true; 
        EnableTextComponent(inputType);
        if (inputType != eDialogueType.tutorial) {
            pickUpOrNpc = true;
        }
    }

    public void DM_EndDialogue(eDialogueType inputType)
    {
        //StartCoroutine(EnableMovementCoroutine());
        playCont.canMove = true;
        inDialogue = false;
        npcTextComponent.enabled = false;
        pickUpOrNpc = false;
        if (dialogueType == "tutorial") {
            // do gamestart things
            tutorialScript.ResetValues(); 
        }
    }
    public void SetDialogueClass(DialogueClass inputs) {
        foreach(var input in inputs){
            
        }

    }
    public void SetDialogueValue(bool value)
    {
        if (dialogueType != null){
            inDialogue = value;
            if (inDialogue) {
                DM_StartDialogue();
            } else if (!inDialogue) {
                DM_EndDialogue();
            }
        } else {
            Debug.LogError("no dialogue type specified");
        }
    }

    public void SetDialogueValue(bool value, eDialogueType inputType)
    {
        inDialogue = value;
        dialogueType = source;

        if (inDialogue) {
            DM_StartDialogue();
        } else if (!inDialogue) {
            DM_EndDialogue();
        }
    }

    public void SetDialogueValue(bool value, eDialogueType inputType, bool end)
    {
        inDialogue = value;
        dialogueType = source;
        if (end){
            if (inDialogue) {
                DM_StartDialogue();
            } else if (!inDialogue) {
                DM_EndDialogue();
            }
        }
    }

    private void EnableTextComponent(eDialogueType inputType)
    {        
        npcTextComponent.transform.parent.position = Camera.main.WorldToScreenPoint(transform.position);
        npcTextComponent.enabled = true;
        dialogueTimer = 0f;
        if (inputType == eDialogueType.pickup){              
            SetDialogueValue(true, "pickup", false);
            npcTextComponent.color = Color.green;
            npcTextComponent.text = pickupTextToDisplay;  
            dialogueWaitTime = 3f;               
            pickUpOrNpc = true;    
        } else if (inputType == eDialogueType.npc) {
            SetDialogueValue(true, "npc", false);            
            npcTextComponent.text = npcTextToDisplay;
            npcTextComponent.color = Color.blue;
            pickUpOrNpc = true;
        } else if (inputType == eDialogueType.goal) {            
            SetDialogueValue(true, "goal");
            npcTextComponent.text = goalCantLeave;
            npcTextComponent.color = goalText.color;
            // replace with a variable
            dialogueWaitTime = 3f;
        } else if (inputType == eDialogueType.tutorial){
            npcTextComponent.color = Color.blue;
            npcTextComponent.text = tutorialScript.ReturnCurrentTutorial();
            dialogueWaitTime = tutorialScript.ReturnWaitTime();
        } else {
            Debug.LogError("no type specified");
        }
    }
}
