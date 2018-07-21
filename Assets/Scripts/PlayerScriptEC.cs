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
    public Text npcTextComponent;
    public NPCScript myNPC;
    public Text goalText;
    public string goalCantLeave;
    public string pickupText;
    public bool hasKey = false;
    bool end = false;
    float timer;
    float dialogueTimer;
    public float dialogueWaitTime = 5f;
    float msgWaitTime = 1.5f;
    public string npcDialogueText;
    EndCubeScript endCube;
    PlayerContollerEC playCont;
    public bool inDialogue { get; private set; }
    bool pickUpOrNpc;
    GameStart tutorialScript;
    string dialogueType;
    ePlayerState playerState;

    // Use this for initialization  
    void Start()
    {
        pickUpOrNpc = false;
        lives = 3;
        if (FindObjectOfType<GameStart>())
        {
            tutorialScript = FindObjectOfType<GameStart>();
        }
        endCube = FindObjectOfType<EndCubeScript>();
        playCont = GetComponent<PlayerContollerEC>();
        endIMG.enabled = false;
        deadIMG.enabled = false;
        click.enabled = false;
        goalText.enabled = false;
        playerState = ePlayerState.idle;

        DialogueManager.instance.dialogueEvent += SetPlayerInDialogue;
    }

    public void SetPlayerInDialogue(sDialogueStruct sDialogueData)
    {
        playerState = ePlayerState.inDialogue;
    }

    public void PassPlayerState()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UILogic();
        HealthLogic();
        DisplayErrorLogic();
        DialogueLogic();

        if (Input.GetKeyDown(KeyCode.A))
        {
            endCube.ProgressGame();
        }
    }

    private void DialogueLogic()
    {
        //Timer for NPC Dialogue 
        if (npcTextComponent.enabled)
        {
            dialogueTimer = dialogueTimer + Time.deltaTime;
            if (dialogueTimer > dialogueWaitTime)
            {
                if (pickUpOrNpc)
                {
                    //SetDialogueValue(false);
                    pickUpOrNpc = false;
                }
                else if (dialogueType == "tutorial")
                {
                    if (tutorialScript.ReturnCurrentTutorialIndex() > 2)
                    {
                        //SetDialogueValue(false);
                    }
                }
            }
        }

        if (!npcTextComponent.enabled)
        {
            if (dialogueTimer >= dialogueWaitTime && inDialogue)
            {
                //SetDialogueValue(false);
            }
        }
        else
        {
            return;
        }
    }

    public void TutorialHack()
    {
        StartCoroutine(EnableMovementCoroutine());
    }

    IEnumerator EnableMovementCoroutine()
    {
        float crWaitTime = .25f;
        if (dialogueType == "tutorial")
        {
            if (tutorialScript.ReturnCurrentTutorialIndex() < 2)
            {
                yield break;
            }
            else if (tutorialScript.ReturnCurrentTutorialIndex() >= 2)
            {
                yield return new WaitForSeconds(crWaitTime);
                playCont.canMove = true;
            }
        }
        else
        {
            yield return new WaitForSeconds(crWaitTime);
            playCont.canMove = true;
        }
    }

    public void Interact(float waitTime, string text)
    {
        dialogueWaitTime = waitTime;
        npcDialogueText = text;
        //EnableTextComponent("npc");
    }

    public void Pickup()
    {
        //EnableTextComponent("pickup");
    }

    public void noKeyCantLeave()
    {
        //EnableTextComponent("goal");
    }

    // Timer for Cant Reach
    private void DisplayErrorLogic()
    {
        if (click.enabled)
        {
            timer = timer + Time.deltaTime;
            if (timer >= msgWaitTime)
            {
                click.enabled = false;
                timer = 0f;
            }
        }
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

    public void CantReach()
    {
        click.text = "Can't Reach";
        click.enabled = true;
        click.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    public void NotFloor()
    {
        click.text = "That's A NotFloor.";
        click.enabled = true;
        click.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    public void End()
    {
        endIMG.enabled = true;
    }
}
