using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherScript : MonoBehaviour
{

    float timer;
    public float timerMax = 4f;
    public bool isTimer = true;
    [SerializeField]
    private EnemyScript prefab;


    // Use this for initialization
    void Start()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.color = new Color(Random.value, Random.value, Random.value);
    }

    void SpawnMe()
    {
        var projectile = prefab.Get<EnemyScript>(this.transform, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            timer = timer + Time.deltaTime;
            if (timer >= timerMax)
            {
                timer = 0f;
                SpawnMe();
            }
        }
    }
}
