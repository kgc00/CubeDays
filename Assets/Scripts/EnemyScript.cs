using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public float movementSpeed = .05f;
    PlayerScriptEC ps;
    float maxHitRecoveryTime = 2f;
    float cooldownTimer;
    bool hasHit = false;
    float timer;
    public float life;
    public bool forward = true;
    public bool right = false;
    public bool back = false;
    public bool left = false;

    // Use this for initialization
    void Start () {
        cooldownTimer = maxHitRecoveryTime;
        ps = FindObjectOfType<PlayerScriptEC>();

        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.color = new Color(Random.value, Random.value, Random.value);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (forward)
        {
            transform.position = transform.position + Vector3.forward * movementSpeed;
        } else if (right)
        {
            transform.position = transform.position + Vector3.right * movementSpeed;            
            transform.rotation = Quaternion.Euler(0, 0, 90);
        } else if (back)
        {
            transform.position = transform.position + Vector3.back * movementSpeed;
        } else if (left)
        {
            transform.position = transform.position + Vector3.left * movementSpeed;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }


        timer = timer + Time.deltaTime;
        if (timer >= life)
        {
            Destroy(gameObject.transform.parent.gameObject);
            timer = 0f;
        }

        //if (hasHit)
        //{
        //    cooldownTimer = cooldownTimer - Time.deltaTime;

        //    if (cooldownTimer <= 0)
        //    {
        //        cooldownTimer = maxHitRecoveryTime;
        //        hasHit = false;
        //    }
        //}
    }

    //public void Test()
    //{
    //    Debug.Log("Worked");
    //}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("1");
        if (other.tag == "Player")// && !hasHit)
        {
           // Debug.Log("2");
            ps.hurt();
            hasHit = true;
        }
    }    
}
