using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : DialogueObject
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InitiateDialogue(sDialogueData);
        }
    }
}
