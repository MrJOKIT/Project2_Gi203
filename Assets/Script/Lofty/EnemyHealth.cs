using System;
using System.Collections;
using System.Collections.Generic;
using BarthaSzabolcs.Tutorial_SpriteFlash;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private GameObject dropSplash;
    private SimpleFlash _simpleFlash;

    [SerializeField]
    private bool isBoss;

    public float Health
    {
        get { return health; }
    }

    private void Start()
    {
        _simpleFlash = GetComponent<SimpleFlash>();
    }

    private void Update()
    {
        if (health <= 0 && !isBoss)
        {
            EnemyDie();
        }
        else if (health <= 0 && isBoss)
        {
            Instantiate(dropSplash, transform.position, Quaternion.identity);
        }
    }

    private void EnemyDie()
    {
        Instantiate(dropSplash, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void EnemyTakeDamage(float damage)
    {
        _simpleFlash.Flash();
        health -= damage;
    }
}
