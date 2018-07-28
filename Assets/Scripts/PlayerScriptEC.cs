using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptEC : MonoBehaviour
{
    PlayerContollerEC playCont;
    public ePlayerState playerState;
    public static PlayerScriptEC instance;
    public PlayerInventory inventory { get; private set; }

    // Use this for initialization  
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

        playCont = GetComponent<PlayerContollerEC>();

        playerState = ePlayerState.idle;

        DialogueManager.instance.initiateDialogueEvent += SetPlayerInDialogue;
        DialogueManager.instance.endDialogueEvent += SetPlayerOutOfDialogue;
        EndCubeScript.instance.endLevelFeedback += DisableMovement;
    }

    private void SetPlayerInDialogue(sDialogueStruct sDialogueData)
    {
        playerState = ePlayerState.inDialogue;
    }

    private void DisableMovement()
    {
        playerState = ePlayerState.inDialogue;
    }

    private void SetPlayerOutOfDialogue()
    {
        playerState = ePlayerState.idle;
    }

    public ePlayerState PassPlayerState()
    {
        return playerState;
    }
}
