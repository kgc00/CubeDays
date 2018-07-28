using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerUI : MonoBehaviour
{
    public Text healthText;
    public Text livesText;
    public string inventoryString;
    public Text inventoryText;
    public Text scoreText;
    public int scoreValue;
    public Image endIMG;
    public Image deadIMG;
    public Text goalText;
    private PlayerHealth healthComponent;
    // Use this for initialization
    void Start()
    {
        endIMG.enabled = false;
        deadIMG.enabled = false;
        goalText.enabled = false;
        healthComponent = GetComponent<PlayerHealth>();
    }

    private void UILogic()
    {
        healthText.text = healthComponent.health.ToString();
        livesText.text = healthComponent.lives.ToString();
        inventoryText.text = inventoryString;
        scoreText.text = scoreValue.ToString();
    }
}
