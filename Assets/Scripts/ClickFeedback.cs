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
    private float timeToWait = 1.5f;
    [SerializeField]
    private Transform target;

    // Use this for initialization
    void Start()
    {
        textToUse.enabled = false;
        PlayerContollerEC.instance.targetInvalid += DetermineErrorType;     
    }

    private IEnumerator DisplayErrorLogic(string messageToDisplay)
    {
        textToUse.transform.position = Camera.main.WorldToScreenPoint(target.position);
        textToUse.enabled = true;
		textToUse.text = messageToDisplay;
		textToUse.color = Color.red;
        yield return new WaitForSeconds(timeToWait);
		ClearFeedback();
		yield return null;
    }

    private void ClearFeedback(){
         textToUse.enabled = false;
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
