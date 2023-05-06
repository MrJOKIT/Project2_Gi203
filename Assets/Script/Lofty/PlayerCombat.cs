using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Stats")] 
    [SerializeField] private float playerDamage;
    [SerializeField] private TextMeshProUGUI attackText;

    [Header("Attack Setting")] 
    [SerializeField] private float attackCost;
    [SerializeField] private KeyCode keyAttack;
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPos;
    public LayerMask enemyLayer;

    [Header("Throw Spear Setting")] 
    [SerializeField] private float throwCost;
    [SerializeField] private float throwSpeed;
    [SerializeField] private Rigidbody2D spearPrefab;
    [SerializeField] private Transform throwPos;
    [SerializeField] private KeyCode keyThrow;

    [Header("Ref")] 
    private PlayerController _playerController;
    private PlayerAnimationState _playerAnimationState;
    private PlayerStats _playerStats;
    private SoundManager _soundManager;

    public float PlayerDamage
    {
        get { return playerDamage; }
        set { playerDamage = value; }
    }
    
    private void Start()
    {
        playerDamage = PlayerPrefs.GetFloat("Atk");
        
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        _playerController = GetComponent<PlayerController>();
        _playerAnimationState = GetComponent<PlayerAnimationState>();
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        attackText.text = $"{Convert.ToInt32(playerDamage)}";
        
        if (Input.GetKeyDown(keyAttack) && _playerStats.Stamina >= attackCost && !_playerController.isAttack && !_playerController.isHurt && !_playerController.isDie)
        {
            _playerStats.Stamina -= attackCost;
            _playerController.isAttack = true;
            if (_playerController.isJumping)
            {
                PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.AirAttack;
                SoundManager.instace.Play(SoundManager.SoundName.Attack);
            }
            else
            {
                PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.Attack;
                SoundManager.instace.Play(SoundManager.SoundName.Attack);
            }
            
        }

        if (Input.GetKeyDown(keyThrow) && _playerStats.Stamina >= throwCost && !_playerController.isAttack && !_playerController.isHurt && !_playerController.isDie)
        {
            _playerStats.Stamina -= throwCost;
            ThrowSpear();
        }
    }

    public void AttackStart()
    {
        _playerController.isAttack = true;
    }

    public void AttackEnd()
    {
        _playerController.isAttack = false;
    }

    public void PlayerAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position,attackRadius,enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().EnemyTakeDamage(playerDamage);
        }
    }

    private void ThrowSpear()
    {
        SoundManager.instace.Play(SoundManager.SoundName.Shoot);
        Rigidbody2D spear = Instantiate(spearPrefab, throwPos.position, throwPos.rotation);
        spear.velocity = spear.transform.right * throwSpeed;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position,attackRadius);
    }
}
