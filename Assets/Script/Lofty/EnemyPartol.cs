using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyPartol : MonoBehaviour
{
    [Header("Patrol Setting")] 
    [SerializeField] private float patrolSpeed;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    private bool onGround;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallDistance;
    [SerializeField] private LayerMask wallMask;
    private bool onWall;
    
    private bool facingRight = true;

    [Header("Enemy Combat")] 
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRadius;
    [SerializeField] private float enemyDamage;
    private bool isAttack;
    

    [Header("HuntMode Setting")] 
    [SerializeField] private float stopDistance;
    [SerializeField] private float delayAttackTimeCounter;
    private float delayAttackTime;
    
    [Header("Detect Setting")] 
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private bool foundPlayer;
    private Transform playerTrans;

    [Header("Ref")] 
    private EnemyAnimationState _enemyAnimationState;

    private void Start()
    {
        _enemyAnimationState = GetComponent<EnemyAnimationState>();
    }

    private void Update()
    {
        foundPlayer = Physics2D.OverlapCircle(transform.position, detectRadius,playerLayer);
        onGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundMask);
        onWall = Physics2D.Raycast(wallCheck.position, Vector2.right, wallDistance, wallMask);
        
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectRadius,playerLayer);
        if (foundPlayer && player.CompareTag("Player"))
        {
            playerTrans = player.transform;
        }
        else if (!foundPlayer && !isAttack)
        {
            playerTrans = null;
            Patrol();
        }

        if (foundPlayer)
        {
            HuntMode();
            EnemyFlip();
            
        }
    }

    private void EnemyFlip()
    {
        if (playerTrans.position.x < transform.position.x)
        {
            facingRight = false;
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else if (playerTrans.position.x > transform.position.x)
        {
            facingRight = true;
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    private void HuntMode()
    {
        float playerDistance = Vector2.Distance(playerTrans.position, transform.position);
        if (playerDistance > stopDistance &&  !isAttack && onGround && !onWall)
        {
            _enemyAnimationState._state = EnemyAnimationState.EnemyState.Walk;
            if (facingRight)
            {
            
                transform.position += Vector3.right * patrolSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.left * patrolSpeed * Time.deltaTime;
            }
            delayAttackTime = 0f;

        }
        else if (playerDistance > stopDistance &&  !isAttack && !onGround && !onWall)
        {
            _enemyAnimationState._state = EnemyAnimationState.EnemyState.Idle;
            delayAttackTime = 0f;
        }
        else if (playerDistance <= stopDistance && !isAttack )
        {
            _enemyAnimationState._state = EnemyAnimationState.EnemyState.Idle;
            delayAttackTime += Time.deltaTime;
            if (delayAttackTime > delayAttackTimeCounter)
            {
                isAttack = true;
                _enemyAnimationState._state = EnemyAnimationState.EnemyState.Attack;
            }
            
        }
    }

    private void Patrol()
    {
        _enemyAnimationState._state = EnemyAnimationState.EnemyState.Walk;
        if (!onGround && facingRight || onWall && facingRight)
        {
            facingRight = false;
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else if (!onGround && !facingRight || onWall && !facingRight)
        {
            facingRight = true;
            transform.rotation = Quaternion.Euler(0,0,0);
        }

        if (facingRight)
        {
            
            transform.position += Vector3.right * patrolSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * patrolSpeed * Time.deltaTime;
        }
        
    }

    public void Attack()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPos.position, attackRadius,playerLayer);
        if (player.CompareTag("Player"))
        {
            player.GetComponent<PlayerStats>().PlayerTakeDamage(enemyDamage);
        }
    }

    public void FinishAttack()
    {
        _enemyAnimationState._state = EnemyAnimationState.EnemyState.Idle;
        delayAttackTime = 0f;
        isAttack = false;
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,detectRadius);
        Gizmos.DrawWireSphere(attackPos.position,attackRadius);
        Gizmos.DrawRay(groundCheck.position,Vector3.down * groundDistance);
        Gizmos.DrawRay(wallCheck.position,Vector3.right * wallDistance);
    }
}
