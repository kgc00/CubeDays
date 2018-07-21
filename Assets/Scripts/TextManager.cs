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
		
        DialogueManager.instance.dialogueEvent += ShowText;
		HideText();
    }

    private void ShowText(sDialogueStruct sDialogueData)
    {
		animator.Play("TextAnimation");
		textParent.SetActive(true);
        textToUse.color = sDialogueData.textColor;
        textToUse.text = sDialogueData.tTextToDisplay;
    }

    private void HideText()
    {
		textParent.SetActive(false);
    }
}
