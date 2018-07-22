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
        DialogueManager.instance.endDialogueEvent += AnimateTextOut;
        HideText();
    }

    private void ShowText(sDialogueStruct sDialogueData)
    {
        animator.SetTrigger("StartText");
        textParent.SetActive(true);
        textToUse.color = sDialogueData.textColor;
        textToUse.text = sDialogueData.tTextToDisplay;
    }

    public void AnimateTextOut()
    {
        animator.SetTrigger("EndText");
        HideText();
    }

    private void HideText()
    {
        textParent.SetActive(false);
    }
}
