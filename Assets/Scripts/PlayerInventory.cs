using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerInventory : MonoBehaviour
{
    public bool hasKey { get; private set; }
    public static PlayerInventory instance;
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
        hasKey = false;
    }

    void Start()
    {
        PickupScript.instance.startCollection += UpdateInventory;
    }

    private void UpdateInventory()
    {
        PickupScript.instance.startCollection -= UpdateInventory;
        hasKey = true;        
    }

    private bool ReturnInventory()
    {
        return hasKey;
    }
}
