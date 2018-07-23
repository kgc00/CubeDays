using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerScriptEC))]
public class DialogueManager : MonoBehaviour
{

    public event Action<sDialogueStruct> initiateDialogueEvent = delegate { };
    public event Action<sDialogueStruct> continueDialogueEvent = delegate { };
    public event Action endDialogueEvent = delegate { };
    public int currentLineOfDialogue;
    private sDialogueStruct currentDialogueData;
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
        continueDialogueEvent = DM_ContinueDialogue;
        endDialogueEvent = DM_EndDialogue;
    }


    void Update()
    {
        if (CheckPlayerInDialogue())
        {
            // DialogueLogic();
        }
    }

    private void UpdateCurrentDialogueData(sDialogueStruct dialogueData)
    {
        currentDialogueData = dialogueData;
    }
    private bool CheckPlayerInDialogue()
    {
        return PlayerScriptEC.instance.playerState == ePlayerState.inDialogue;
    }

    public void TryStartDialogue(sDialogueStruct sDialogueData)
    {
        if (PlayerScriptEC.instance.playerState != ePlayerState.inDialogue)
        {
            if (initiateDialogueEvent != null)
            {                
                initiateDialogueEvent(sDialogueData);
            }
            else
            {
                Debug.Log("couldnt start dialogue");
            }
        }
    }

    public void TryEndDialogue()
    {
        if (!CheckDialogueLength(currentDialogueData, currentLineOfDialogue))
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
        else
        {
            continueDialogueEvent(currentDialogueData);
        }
    }

    private void DM_StartDialogue(sDialogueStruct sDialogueData)
    {
        currentLineOfDialogue = 0;
        UpdateCurrentDialogueData(sDialogueData);
        StopCoroutine(ContinueDialogueAfterWait(sDialogueData, CheckContinueDialouge));
        StartCoroutine(ContinueDialogueAfterWait(sDialogueData, CheckContinueDialouge));
    }

    private void DM_ContinueDialogue(sDialogueStruct sDialogueData)
    {
        IncreaseDialogueLine();
        StartCoroutine(ContinueDialogueAfterWait(sDialogueData, CheckContinueDialouge));
    }

    private IEnumerator ContinueDialogueAfterWait(sDialogueStruct sDialogueData, Action<sDialogueStruct, int> onComplete)
    {
        yield return new WaitForSeconds(sDialogueData.fDialogueWaitTime);
        onComplete(sDialogueData, currentLineOfDialogue);
        yield return null;
    }

    private void CheckContinueDialouge(sDialogueStruct dialogueData, int currentLine)
    {
        if (CheckDialogueLength(dialogueData, currentLine))
        {
            continueDialogueEvent(dialogueData);
        }
        else
        {
            TryEndDialogue();
        }
    }

    private static bool CheckDialogueLength(sDialogueStruct dialogueData, int currentLine)
    {
        return dialogueData.tTextToDisplay.Length > currentLine;
    }

    private void IncreaseDialogueLine()
    {
        currentLineOfDialogue += 1;
    }

    private void DM_EndDialogue()
    {
    }
}
