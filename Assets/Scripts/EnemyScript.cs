using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : PooledMonobehaviour
{
    public event Action dealDamage;
    public float movementSpeed = .05f;
    float maxHitRecoveryTime = 2f;
    float cooldownTimer;
    bool hasHit = false;
    [SerializeField]
    private float lifespan = 4.0f;

    void Awake()
    {
        cooldownTimer = maxHitRecoveryTime;
        Renderer rend = gameObject.GetComponentInChildren<Renderer>();
        rend.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        dealDamage = DealDamage;
    }

    private void OnEnable(){
        StartCoroutine(DisableSelf());
    }

    private IEnumerator DisableSelf(){
        yield return new WaitForSeconds(lifespan);
        base.OnDisable();
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position = transform.position + transform.forward * movementSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dealDamage();
            hasHit = true;
        }
    }

    private void DealDamage()
    {
        // stuff
    }
}
