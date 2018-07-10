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
    public bool inDialogue {get; private set; }
    bool pickUpOrNpc;
    GameStart tutorialScript;
    string dialogueType;

    // Use this for initialization  
    void Start()
    {
        pickUpOrNpc = false;
        lives = 3;
        if (FindObjectOfType<GameStart>()){
            tutorialScript = FindObjectOfType<GameStart>();
        }
        endCube = FindObjectOfType<EndCubeScript>();
        playCont = GetComponent<PlayerContollerEC>();
        endIMG.enabled = false;
        deadIMG.enabled = false;
        click.enabled = false;
        goalText.enabled = false;
        inDialogue = false;
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
            if (dialogueTimer > dialogueWaitTime){
                if (pickUpOrNpc) {
                    SetDialogueValue(false);
                    pickUpOrNpc = false;
                } else if (dialogueType == "tutorial") {
                    if (tutorialScript.ReturnCurrentTutorialIndex() > 2) {
                        SetDialogueValue(false);
                    }
                }
            }
        }

        if (!npcTextComponent.enabled) 
        {
            if (dialogueTimer >= dialogueWaitTime && inDialogue){
                SetDialogueValue(false);
            }
        } else {
            return;
        }
    }

    public void TutorialHack(){
        StartCoroutine(EnableMovementCoroutine());
    }

    IEnumerator EnableMovementCoroutine(){
        float crWaitTime = .25f;
        if (dialogueType == "tutorial") {
            if (tutorialScript.ReturnCurrentTutorialIndex() < 2) {
                yield break;
            } else if (tutorialScript.ReturnCurrentTutorialIndex() >= 2) {
                yield return new WaitForSeconds(crWaitTime);        
                playCont.canMove = true;
            }
        } else{
            yield return new WaitForSeconds(crWaitTime);        
            playCont.canMove = true;
        }
    }

    public void PS_StartDialogue(){
        playCont.canMove = false;
        inDialogue = true;
        npcTextComponent.enabled = true; 
        EnableTextComponent(dialogueType);
        if (dialogueType != "tutorial") {
            pickUpOrNpc = true;
        }
    }

    public void PS_EndDialogue()
    {
        StartCoroutine(EnableMovementCoroutine());
        // playCont.canMove = true;
        inDialogue = false;
        npcTextComponent.enabled = false;
        pickUpOrNpc = false;
        if (dialogueType == "tutorial") {
            // do gamestart things
            tutorialScript.ResetValues(); 
        }
    }
    
    public void SetDialogueValue(bool value)
    {
        if (dialogueType != null){
            inDialogue = value;
            if (inDialogue) {
                PS_StartDialogue();
            } else if (!inDialogue) {
                PS_EndDialogue();
            }
        } else {
            Debug.LogError("no dialogue type specified");
        }
    }

    public void SetDialogueValue(bool value, string source)
    {
        inDialogue = value;
        dialogueType = source;

        if (inDialogue) {
            PS_StartDialogue();
        } else if (!inDialogue) {
            PS_EndDialogue();
        }
    }

        public void SetDialogueValue(bool value, string source, bool end)
    {
        inDialogue = value;
        dialogueType = source;
        if (end){
            if (inDialogue) {
                PS_StartDialogue();
            } else if (!inDialogue) {
                PS_EndDialogue();
            }
        }
    }

    private void EnableTextComponent(string type)
    {        
        npcTextComponent.transform.parent.position = Camera.main.WorldToScreenPoint(transform.position);
        npcTextComponent.enabled = true;
        dialogueTimer = 0f;
        if (type == "pickup"){              
            SetDialogueValue(true, "pickup", false);
            npcTextComponent.color = Color.green;
            npcTextComponent.text = pickupText;  
            dialogueWaitTime = 3f;               
            pickUpOrNpc = true;    
        } else if (type == "npc") {
            SetDialogueValue(true, "npc", false);            
            npcTextComponent.text = npcDialogueText;
            npcTextComponent.color = Color.blue;
            pickUpOrNpc = true;
        } else if (type == "goal") {            
            SetDialogueValue(true, "goal");
            npcTextComponent.text = goalCantLeave;
            npcTextComponent.color = goalText.color;
            // replace with a variable
            dialogueWaitTime = 3f;
        } else if (type == "tutorial"){
            npcTextComponent.color = Color.blue;
            npcTextComponent.text = tutorialScript.ReturnCurrentTutorial();
            dialogueWaitTime = tutorialScript.ReturnWaitTime();
        } else {
            Debug.LogError("no type specified");
        }
    }

    private void DisplayErrorLogic()
    {
        //Timer for Cant Reach
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

    public void Interact(float waitTime, string text)
    {
        dialogueWaitTime = waitTime;
        npcDialogueText = text;
        EnableTextComponent("npc");
    }

    public void Pickup()
    {
        EnableTextComponent("pickup");
    }

    public void noKeyCantLeave()
    {
        EnableTextComponent("goal");
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
