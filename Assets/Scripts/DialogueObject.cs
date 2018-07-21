using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : MonoBehaviour {

	[SerializeField]
	protected sDialogueStruct sDialogueData;

	protected void InitiateDialogue(sDialogueStruct sDialogueData){
		DialogueManager.instance.ReceiveDialogue(sDialogueData);
	}
}
