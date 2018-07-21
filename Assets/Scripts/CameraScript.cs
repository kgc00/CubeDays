using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public int camNum = 1;
	[SerializeField]
	GameStart gs;

    void Start()
    {
    }

    void Update()
    { 
    }

    public void ChangeCamera()
    {
        // Debug.Log("ChangeCamera: " + camNum);
        if (camNum == 1)
        {
            transform.position = pos1.position;
            transform.rotation = (pos1.rotation);
        }
        else if (camNum == 2)
        {
            transform.position = pos2.position;
            transform.rotation = (pos2.rotation);
        }
        else if (camNum == 3)
        {
            transform.position = pos3.position;
            transform.rotation = (pos3.rotation);
        }
        else if (camNum == 4)
        {
            transform.position = pos4.position;
            transform.rotation = (pos4.rotation);
        }
		if (gs) {
			gs.Tutorial ();
		}
    } 
}