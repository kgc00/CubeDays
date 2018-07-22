using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerScriptEC))]
public class DialogueManager : MonoBehaviour
{

    public event Action<sDialogueStruct> initiateDialogueEvent = delegate { };
    public event Action endDialogueEvent = delegate { };

    public static DialogueManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        initiateDialogueEvent = DM_StartDialogue;
        endDialogueEvent = DM_EndDialogue;
    }


    void Update()
    {
        if (CheckPlayerInDialogue())
        {
            DialogueLogic();
        }
    }

    private bool CheckPlayerInDialogue()
    {
        return PlayerScriptEC.instance.playerState == ePlayerState.inDialogue;
    }

    private void DialogueLogic()
    {
        
    }

    public void TryStartDialogue(sDialogueStruct sDialogueData)
    {
        if (initiateDialogueEvent != null)
        {
            initiateDialogueEvent(sDialogueData);
        } else {
            Debug.Log("couldnt start dialogue");
        }
    }

    public void TryEndDialogue()
    {
        if (endDialogueEvent != null)
        {
            endDialogueEvent();
        }
        else
        {
            Debug.LogError("No end dialogue event");
        }
    }

    public void DM_StartDialogue(sDialogueStruct sDialogueData)
    {
        // enable text
        // start timer (set state for player)
        StartCoroutine(SetDialogueLength(sDialogueData.fDialogueWaitTime));
        // disable movement
    }

    private IEnumerator SetDialogueLength(float dialogueLength)
    {
        yield return new WaitForSeconds(dialogueLength);
        if (CheckContinueDialouge())
        {

        }
        else
        {
            TryEndDialogue();
        }
    }

    private bool CheckContinueDialouge()
    {
        // have to implement
        return false;
        //PlayerScriptEC.instance.playerState == ePlayerState.inDialogue;
    }

    public void DM_EndDialogue()
    {
        // reset timer (and player state)
        // enable moevemnt
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
