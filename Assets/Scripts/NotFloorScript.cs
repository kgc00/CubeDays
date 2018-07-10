using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotFloorScript : MonoBehaviour {
    public Material notFloorMat;
    public Material floorMat;
    Material currentMat;
    bool newMat;
    //public LayerMask floorLayerMask;
    //public LayerMask notFloorLayerMask;
    Renderer rend;
    public bool isTimed;
    float timer;
    public float timerMax = 2f;

    // Use this for initialization
    void Start ()
    {
        rend = gameObject.GetComponent<Renderer>();
        currentMat = notFloorMat;
    }
	
	// Update is called once per frame
	void Update ()
    {  
        if (newMat)
        {
            rend.material = currentMat;
        }
        if (isTimed)
        {
            timer = timer + Time.deltaTime;
            if (timer >= timerMax)
            {
                SwitchMat();
                timer = 0f;
            }
        }
    }

    public void SwitchMat()
    {
        if (currentMat == floorMat)
        {
            currentMat = notFloorMat;
            gameObject.layer = 14;
            newMat = true;
        } else if (currentMat == notFloorMat)
        {
            currentMat = floorMat;
            gameObject.layer = 9;
            newMat = true;
        } else if (currentMat == null)
        {
            currentMat = notFloorMat;
            newMat = true;
        }
    }
}
