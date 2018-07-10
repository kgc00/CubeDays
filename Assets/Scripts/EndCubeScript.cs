using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCubeScript : MonoBehaviour
{

    public PlayerContollerEC playController;
    public PlayerScriptEC playScript;
    public GameObject sparklyThing;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    bool showText = false;
    float timer;
    float loadTime = 3f;
    bool triggered = false;
    public string level;
    bool hasKey = false;
    public bool isEnd = false;

    // Use this for initialization
    void Start()
    {
        playController = GameObject.FindObjectOfType<PlayerContollerEC>();
        playScript = GameObject.FindObjectOfType<PlayerScriptEC>();
    }

    void Update()
    {
        if (triggered)
        {
            timer = timer + Time.deltaTime;
        }
        
        if (timer >= loadTime)
        {
            if (isEnd)
            {
                playScript.End();
                playController.canMove = false;
            } else
            {
                SceneManager.LoadScene(level);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hasKey = playScript.hasKey;
            if (hasKey)
            {
                ProgressGame ();
            } else if (!hasKey)
            {
                playScript.noKeyCantLeave();
            }
        }
    }

	public void ProgressGame ()
	{
		playController.canMove = false;
		triggered = true;
		playScript.goalText.enabled = true;
		playScript.goalText.transform.parent.gameObject.GetComponent<Image> ().enabled = true;
		GameObject sparkly = Instantiate (sparklyThing, spawn1.transform.position, Quaternion.Euler (-90, 0, 0));
		sparkly = Instantiate (sparklyThing, spawn2.transform.position, Quaternion.Euler (-90, 0, 0));
		sparkly = Instantiate (sparklyThing, spawn3.transform.position, Quaternion.Euler (-90, 0, 0));
		sparkly = Instantiate (sparklyThing, spawn4.transform.position, Quaternion.Euler (-90, 0, 0));
	}
}