using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherScript : MonoBehaviour {

    //Adding a panel behind npcText
    //rotation of confetti

    float timer;
    public float timerMax = 4f;
    public GameObject enemy;
    public bool isTimer = true;
    public EnemyScript enemyScript;
    public float myEnemyLife = 6f;
    public bool myEnemyForward = true;
    public bool myEnemyRight = false;
    public float myEnemySpeed = .5f;
    public bool myEnemyLeft = false;
    public bool myEnemyBack = false;


    // Use this for initialization
    void Start () {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.color = new Color(Random.value, Random.value, Random.value);
    }
	
	// Update is called once per frame
	void Update () {
        if (isTimer)
        { 
            timer = timer + Time.deltaTime;
            if (timer >= timerMax)
            {
                timer = 0f;
                SpawnEnemy();
            }
        }
	}

    void SpawnEnemy ()
    {
        GameObject enem1 = Instantiate(enemy, this.transform.position, Quaternion.identity);
        enemyScript = enem1.GetComponentInChildren<EnemyScript>();
        enemyScript.life = myEnemyLife;
        enemyScript.forward = myEnemyForward;
        enemyScript.right = myEnemyRight;
        enemyScript.left = myEnemyLeft;
        enemyScript.back = myEnemyBack;
        enemyScript.movementSpeed = myEnemySpeed;
    }
}
