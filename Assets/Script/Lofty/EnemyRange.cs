using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float bulletSpeed;
    

    [Header("Detect Setting")] 
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask playerLayer;
    private Transform playerTransform;
    private bool foundPlayer;

    [Header("Attack Setting")] 
    [SerializeField] private float delayAttackTimeCounter;
    private float delayAttackTime;
    

    private void Update()
    {
        foundPlayer = Physics2D.OverlapCircle(transform.position, detectRadius, playerLayer);
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectRadius, playerLayer);
        if (foundPlayer && player.CompareTag("Player"))
        {
            playerTransform = player.gameObject.transform;
        }
        else if (!foundPlayer)
        {
            playerTransform = null;
        }

        if (foundPlayer)
        {
            delayAttackTime += Time.deltaTime;
            if (delayAttackTime > delayAttackTimeCounter)
            {
                Shoot();
                delayAttackTime = 0f;
            }
        }
    }

    private void Shoot()
    {
        Vector2 projectileVelocity = 
            CalculateProjectileVelocity(shootPosition.position, playerTransform.position, bulletSpeed);
        Rigidbody2D bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        bullet.velocity = projectileVelocity;
    }
    
    Vector2 CalculateProjectileVelocity( Vector2 origin , Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float disX = distance.x;
        float disY = distance.y;

        float velocityX = disX / time;
        float velocityY = disY / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        Vector2 result = new Vector2(velocityX, velocityY);

        return result;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,detectRadius);
    }
}
