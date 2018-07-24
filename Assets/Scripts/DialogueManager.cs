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
    private IEnumerator CheckContinue;

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
            print ("try end dialogue was called");
            StopCoroutine(ContinueDialogueAfterWait(currentDialogueData, CheckContinueDialouge));
            continueDialogueEvent(currentDialogueData);
        }
    }

    private void DM_StartDialogue(sDialogueStruct sDialogueData)
    {
        currentLineOfDialogue = 0;
        UpdateCurrentDialogueData(sDialogueData);
        if (CheckContinue != null)
        {
            StopCoroutine(CheckContinue);
        }
        CheckContinue = ContinueDialogueAfterWait(sDialogueData, CheckContinueDialouge);
        StartCoroutine(CheckContinue);
    }

    private void DM_ContinueDialogue(sDialogueStruct sDialogueData)
    {        
        if (CheckContinue != null)
        {
            StopCoroutine(CheckContinue);
        }
        StartCoroutine(CheckContinue);
    }

    private IEnumerator ContinueDialogueAfterWait(sDialogueStruct sDialogueData, Action<sDialogueStruct, int> onComplete)
    {
        float counter = 0;
        while (true)
        {
            if (counter >= sDialogueData.fDialogueWaitTime)
            {
                onComplete(sDialogueData, currentLineOfDialogue);
                break;
            }
            counter += Time.deltaTime;
            yield return null;
        }
    }

    private void CheckContinueDialouge(sDialogueStruct dialogueData, int currentLine)
    {
        IncreaseDialogueLine();
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
        // - 1 beacuse we feed currentline directly into our textmanager.
        // (which matches the int to the index in sDialogueData array of strings)
        return dialogueData.tTextToDisplay.Length - 1 > currentLine;
    }

    private void IncreaseDialogueLine()
    {
        currentLineOfDialogue += 1;
    }

    private void DM_EndDialogue()
    {
    }
}
