using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContollerEC : MonoBehaviour
{

    CharacterController cc;

    public int score = 0;
    float moveSpeed = 5f;
    public bool canMove = true;
    public LayerMask interactableMask;
    public LayerMask npcMask;
    public LayerMask geometryMask;
    public LayerMask notFloorMask;

    Vector3 destination;
    bool hasADestination = false;
    bool hitSomethingNF;

    public GameObject destinationSparkle;
    public GameObject myPlay;
    GameStart tutorialScript;
    bool moveClick;

    // Use this for initialization
    void Start()
    {
        if (FindObjectOfType<GameStart>())
        {
            tutorialScript = FindObjectOfType<GameStart>();
        }
        moveClick = false;
        cc = gameObject.GetComponent<CharacterController>();
        PlayerScriptEC.instance = gameObject.GetComponent<PlayerScriptEC>();
    }



    void Update()
    {
        DeathLogic();
        DetermineLogic();
    }

    private void DetermineLogic()
    {
        if (CheckPlayerInDialogue())
        {
            DialogueLogic();
        }
        else
        {
            MovementLogic();
        }
    }

    private void DialogueLogic()
    {
        if (CheckPlayerInDialogue())
        {
            if (Input.GetMouseButtonDown(0))
            {
                DialogueManager.instance.TryEndDialogue();
                return;
            }
        }
    }

    private bool CheckPlayerInDialogue()
    {
        return PlayerScriptEC.instance.playerState == ePlayerState.inDialogue;
    }

    private void MovementLogic()
    {
        RayCastLogic();
        DestinationLogic();
    }

    private void RayCastLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            RaycastHit hitnotFloor;
            bool hitSomething = Physics.Raycast(ray, out hit, 1000, geometryMask);
            hitSomethingNF = Physics.Raycast(ray, out hitnotFloor, 1000, notFloorMask);

            if (hit.point.y - transform.position.y > .6f || hit.point.y - transform.position.y < -2f)
            {
                //Debug.Log("Click");
                PlayerScriptEC.instance.CantReach();
            }
            else if (hitSomething)
            {
                //Store the position that we clicked (in the 'destination' variable)
                destination = hit.point;
                destination.y = hit.point.y + .7f;  //myPlay.transform.position.y;
                hasADestination = true;

                //Instantiate a notification of where you clicked
                GameObject sparkle = (GameObject)Instantiate(destinationSparkle, destination, Quaternion.identity);
                Destroy(sparkle, 3);
            }
            else if (hitSomethingNF)
            {
                PlayerScriptEC.instance.NotFloor();
            }
        }
    }

    private void DestinationLogic()
    {
        if (hasADestination)
        {
            //Rotate toward the destination
            transform.LookAt(destination);

            //Move toward the destination (which is forward because of the LookAt command above)
            transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;

            //If we are close to the destination, stop moving toward the destination
            float distanceToDestination = Vector3.Distance(transform.position, destination);
            if (distanceToDestination < 0.1f)
            {
                hasADestination = false;
            }
        }
    }

    private void DeathLogic()
    {
        if (PlayerScriptEC.instance.isDead)
        {
            canMove = false;
        }
    }
}