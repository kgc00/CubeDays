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
        animator.SetTrigger("StartDialogue");
        textParent.SetActive(true);
    }

    private void ContinueDialogue(sDialogueStruct sDialogueData)
    {
        animator.SetTrigger("ContinueDialogue");
        SetText(sDialogueData);
    }

    private void AnimateTextOut()
    {
        animator.SetTrigger("EndDialogue");
        HideText();
    }

    private void HideText()
    {
        textParent.SetActive(false);
    }

    private void SetText(sDialogueStruct sDialogueData)
    {
        textToUse.color = new Color (sDialogueData.tTextColor.r, sDialogueData.tTextColor.g, sDialogueData.tTextColor.b);
        textToUse.text = sDialogueData.tTextToDisplay[GetCurrentLine()];
    }

    private int GetCurrentLine()
    {
        return DialogueManager.instance.currentLineOfDialogue;
    }
}
