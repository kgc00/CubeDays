using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EndCubeScript))]
public class EndLevelUI : MonoBehaviour
{
    [SerializeField]
    private Text textToUse;
    [SerializeField]
    private string messageToDisplay;
    [SerializeField]
    private string alertText;
    [SerializeField]
    private Color textColor;
    [SerializeField]
    private Color alertColor;
    [SerializeField]
    private Transform target;
    private float timeToWait = 3.0f;


    void Awake()
    {
        textToUse.enabled = false;
        EndCubeScript.instance.endLevelFeedback += DisplayEndingText;
        EndCubeScript.instance.failedToEndLevel += DisplayAlertText;
        if (!target)
        {
            try
            {
                target = GameObject.FindWithTag("TextTarget").transform;
            }
            catch
            {
                Debug.LogError("No target found for text.");
            }
        }
    }

    private void DisplayEndingText()
    {
        try
        {
            textToUse.transform.position = Camera.main.WorldToScreenPoint(target.position);
            textToUse.enabled = true;
            textToUse.text = messageToDisplay;
            textToUse.color = textColor;
        }
        catch
        {
            Debug.LogError("text target transform was never set.");
        }
    }

    private void DisplayAlertText()
    {        
        try
        {
            textToUse.transform.position = Camera.main.WorldToScreenPoint(target.position);
            textToUse.enabled = true;
            textToUse.text = alertText;
            textToUse.color = alertColor;
            StartCoroutine(HideText());
        }
        catch
        {
            Debug.LogError("text target transform was never set.");
        }
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(timeToWait);
        textToUse.enabled = false;
    }
}