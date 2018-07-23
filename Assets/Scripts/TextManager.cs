using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    Text textToUse;
    [SerializeField]
    GameObject textParent;
    [SerializeField]
    Animator animator;
    private void Start()
    {
        DialogueManager.instance.initiateDialogueEvent += ShowText;
        DialogueManager.instance.continueDialogueEvent += ContinueDialogue;
        DialogueManager.instance.endDialogueEvent += AnimateTextOut;
        HideText();
    }

    private void ShowText(sDialogueStruct sDialogueData)
    {        
        DialogueManager.instance.initiateDialogueEvent -= ShowText;
        animator.SetTrigger("StartDialogue");
        textParent.SetActive(true);
        SetText(sDialogueData);
    }

    private void SetText(sDialogueStruct sDialogueData)
    {
        textToUse.color = sDialogueData.tTextColor;
        textToUse.text = sDialogueData.tTextToDisplay[GetCurrentLine()];
    }

    private int GetCurrentLine()
    {
        return DialogueManager.instance.currentLineOfDialogue;
    }

    private void AnimateTextOut()
    {
        DialogueManager.instance.endDialogueEvent -= AnimateTextOut;        
        // animator.ResetTrigger("ContinueDialogue");
        // animator.ResetTrigger("StartDialogue");
        animator.SetTrigger("EndDialogue");
        HideText();
    }

    private void ContinueDialogue(sDialogueStruct sDialogueData)
    {
        DialogueManager.instance.continueDialogueEvent -= ContinueDialogue;
        animator.SetTrigger("ContinueDialogue");
        SetText(sDialogueData);
    }

    private void HideText()
    {
        textParent.SetActive(false);
    }
}
