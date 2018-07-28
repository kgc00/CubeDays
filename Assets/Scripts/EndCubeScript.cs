using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class EndCubeScript : MonoBehaviour
{
    public event Action endLevelFeedback = delegate {};
    public event Action failedToEndLevel = delegate {};
    public GameObject sparklyThing;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public string level;
    public static EndCubeScript instance;

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

        endLevelFeedback = DisplayEndingEffects;
    }

    private void OnTriggerEnter(Collider other)
    {
        TryEndLevel();
    }

    private void TryEndLevel()
    {
        if (CheckEndLevel())
        {
            StartCoroutine(EndLevelLogic());
        } else {
            failedToEndLevel();
        }
    }

    private bool CheckEndLevel()
    {
        return PlayerInventory.instance.hasKey;
    }

    private IEnumerator EndLevelLogic()
    {
        endLevelFeedback();
        yield return new WaitForSeconds(4.0f);
        ProgressGame();
        yield return null;
    }

    private void DisplayEndingEffects()
    {
        GameObject sparkly = Instantiate(sparklyThing, spawn1.transform.position, Quaternion.Euler(-90, 0, 0));
        sparkly = Instantiate(sparklyThing, spawn2.transform.position, Quaternion.Euler(-90, 0, 0));
        sparkly = Instantiate(sparklyThing, spawn3.transform.position, Quaternion.Euler(-90, 0, 0));
        sparkly = Instantiate(sparklyThing, spawn4.transform.position, Quaternion.Euler(-90, 0, 0));
    }

    public void ProgressGame()
    {
        SceneManager.LoadScene(level);
    }
}