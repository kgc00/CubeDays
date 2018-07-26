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
    private Coroutine CoroutineHandler;
    private bool exitCoroutine = false;

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

    private void UpdateCurrentDialogueData(sDialogueStruct dialogueData)
    {
        currentDialogueData = dialogueData;
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

    public void SetExitCoroutine()
    {
        exitCoroutine = true;
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
            CheckContinueDialouge(currentDialogueData, currentLineOfDialogue);
        }
    }

    private void DM_StartDialogue(sDialogueStruct sDialogueData)
    {       
        currentLineOfDialogue = 0;
        UpdateCurrentDialogueData(sDialogueData);
        continueDialogueEvent(sDialogueData);        
    }

    private void DM_ContinueDialogue(sDialogueStruct sDialogueData)
    {
        if (CoroutineHandler != null)
        {
            StopCoroutine(CoroutineHandler);
        }
        exitCoroutine = false;
        CoroutineHandler = StartCoroutine(ContinueDialogueAfterWait(sDialogueData, CheckContinueDialouge));        
    }

    private IEnumerator ContinueDialogueAfterWait(sDialogueStruct sDialogueData, Action<sDialogueStruct, int> onComplete)
    {
        float counter = 0;
        while (true)
        {
            if (exitCoroutine)
            {
                onComplete(sDialogueData, currentLineOfDialogue);
                break;
            }

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

    private void DM_EndDialogue()
    {
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
}
