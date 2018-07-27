using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerContollerEC))]
public class ClickFeedback : MonoBehaviour
{
	[SerializeField]
	private string notFloorMessage;
	[SerializeField]
	private string cantReachMessage;
	[SerializeField]
    private Text textToUse;
    Coroutine coroutineHandler;

    // Use this for initialization
    void Start()
    {
        PlayerContollerEC.instance.targetInvalid += DetermineErrorType;
    }

    private IEnumerator DisplayErrorLogic(string messageToDisplay)
    {
        textToUse.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        textToUse.enabled = true;
		textToUse.text = messageToDisplay;
		textToUse.color = Color.red;
		
		// need to add support for additional animation logic/states

		yield return null;
    }

    private void DetermineErrorType(eFloorType _floorType)
    {
        if (coroutineHandler != null)
        {
            StopCoroutine(coroutineHandler);
        }

        string messageToDisplay = "";
        if (_floorType == eFloorType.floor)
        {
			messageToDisplay = cantReachMessage;
        }
        else if (_floorType == eFloorType.notFloor)
        {
            messageToDisplay = notFloorMessage;
        }
		coroutineHandler = StartCoroutine(DisplayErrorLogic(messageToDisplay));
    }
}
