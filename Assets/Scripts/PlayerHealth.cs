using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerScriptEC))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject hurtfx;
    public int lives { get; private set; }
    public int health { get; private set; }

    void Awake()
    {
        lives = 3;
        health = 2;
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
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        // isDead = true;
        // deadIMG.enabled = true;
    }

    public void hurt()
    {
        health--;
        GameObject hurt = Instantiate(hurtfx, transform.position, transform.rotation);
    }
}
