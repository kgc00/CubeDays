using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptEC : MonoBehaviour
{

    public int lives = 3;
    public int health = 2;
    public bool isDead = false;
    public Text healthText;
    public Text livesText;
    public GameObject hurtfx;
    public string inventoryString;
    public Text inventoryText;
    public Text scoreText;
    public Text click;
    public int scoreValue;
    public Image endIMG;
    public Image deadIMG;
    public Text goalText;
    public string goalCantLeave;
    public string pickupText;
    public bool hasKey = false;
    EndCubeScript endCube;
    PlayerContollerEC playCont;
    public ePlayerState playerState;
    public static PlayerScriptEC instance;

    // Use this for initialization  
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        lives = 3;
        endCube = FindObjectOfType<EndCubeScript>();
        playCont = GetComponent<PlayerContollerEC>();
        endIMG.enabled = false;
        deadIMG.enabled = false;
        click.enabled = false;
        goalText.enabled = false;
        playerState = ePlayerState.idle;

        DialogueManager.instance.initiateDialogueEvent += SetPlayerInDialogue;
        DialogueManager.instance.endDialogueEvent += SetPlayerOutOfDialogue;
    }

    private void SetPlayerInDialogue(sDialogueStruct sDialogueData)
    {
        // DialogueManager.instance.initiateDialogueEvent -= SetPlayerInDialogue;
        playerState = ePlayerState.inDialogue;
    }

    private void SetPlayerOutOfDialogue()
    {
        // DialogueManager.instance.endDialogueEvent -= SetPlayerOutOfDialogue;
        playerState = ePlayerState.idle;
    }

    public ePlayerState PassPlayerState()
    {
        return playerState;
    }

    // Update is called once per frame
    void Update()
    {
        UILogic();
        HealthLogic();
        // DisplayErrorLogic();
        // DialogueLogic();

        if (Input.GetKeyDown(KeyCode.A))
        {
            endCube.ProgressGame();
        }
    }

    public void Pickup()
    {
        //EnableTextComponent("pickup");
    }

    public void noKeyCantLeave()
    {
        //EnableTextComponent("goal");
    }

    private void HealthLogic()
    {
        if (health <= 0)
        {
            lives--;
            health = 2;
        }
        else if (lives < 0)
        {
            isDead = true;
            deadIMG.enabled = true;
        }
    }

    private void UILogic()
    {
        healthText.text = health.ToString();
        livesText.text = lives.ToString();
        inventoryText.text = inventoryString;
        scoreText.text = scoreValue.ToString();
    }

    public void hurt()
    {
        health--;
        GameObject hurt = Instantiate(hurtfx, transform.position, transform.rotation);
    }



    public void End()
    {
        endIMG.enabled = true;
    }
}
