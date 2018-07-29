using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text inventoryText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private string currentItem;
    private int scoreValue;
    private PlayerHealth healthComponent;
    // Use this for initialization
    void Awake()
    {
        healthComponent = GetComponent<PlayerHealth>();

    }

    void Start (){
        SetValues();
    }

    private void SetValues()
    {
        UILogic();
        PickupScript.instance.startCollection += GrabbedItem;
    }

    private void UILogic()
    {
        healthText.text = healthComponent.health.ToString();
        livesText.text = healthComponent.lives.ToString();
        scoreText.text = scoreValue.ToString();
    }

    private void GrabbedItem(){
        inventoryText.text = currentItem;
    }
}
