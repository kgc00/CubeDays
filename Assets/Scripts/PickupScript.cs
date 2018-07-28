using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupScript : MonoBehaviour
{
    Renderer rend;
    public event Action startCollection;
    public static PickupScript instance;
    [SerializeField]
    private Color transparent;
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
        rend = gameObject.GetComponent<Renderer>();
        rend.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        startCollection = Collect;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            startCollection();
        }
    }

    private void Collect()
    {
        rend.material.color = transparent;
        Destroy(gameObject, 3f);
    }
}
