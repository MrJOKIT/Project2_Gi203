using System;
using System.Collections;
using System.Collections.Generic;
using BarthaSzabolcs.Tutorial_SpriteFlash;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Player HP Stats")] 
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private Image hpImage;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private GameObject deadPanel;
    
    [Header("Player SP Stats")]
    [SerializeField] private float maxStamina;
    [SerializeField] private float stamina;
    [SerializeField] private float regenSpeed;
    [SerializeField] private Image spImage;
    [SerializeField] private TextMeshProUGUI spText;

    [Header("Ref")] 
    private PlayerAnimationState _state;
    private PlayerController _playerController;
    private PlayerCombat _playerCombat;
    private SimpleFlash _simpleFlash;

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float MaxStamina
    {
        get { return maxStamina; }
        set { maxStamina = value; }
    }
    
    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }
    private void Start()
    {
        maxHealth = PlayerPrefs.GetFloat("Health");
        maxStamina = PlayerPrefs.GetFloat("Stamina");
        
        health = maxHealth;
        stamina = maxStamina;
        
        _state = GetComponent<PlayerAnimationState>();
        _playerController = GetComponent<PlayerController>();
        _simpleFlash = GetComponent<SimpleFlash>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        HpHudCalculate();
        SpHudCalculate();
    }

    public void PlayerTakeDamage(float damage)
    {

        if (!_playerController.isDie)
        {
            _playerController.isHurt = true;
            health -= damage;
            ShakeCamera.instance.StartShake(0.25f,0.5f);

            if (_playerController.isAttack )
            {
                _simpleFlash.Flash();
                _playerController.isHurt = false;
            }
            else
            {
                PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.Hurt;
            }
        }
    }

    public void PlayerTakeHeath(float healthTake)
    {
        if (!_playerController.isDie)
        {
            health += healthTake;
        }
    }

    private void HpHudCalculate()
    {
        hpText.text = $"{maxHealth}";
        hpImage.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            Die();
            health = 0;
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void SpHudCalculate()
    {
        spText.text = $"{maxStamina}";
        spImage.fillAmount = stamina / maxStamina;
        if (stamina < 0)
        {
            stamina = 0;
        }
        else if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }

        if (stamina < maxStamina && !_playerController.isAttack )
        {
            stamina += regenSpeed * Time.deltaTime;
        }
    }

    private void Die()
    {
        _playerController.isDie = true;
        PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.Idle;
        PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.Die;
        deadPanel.SetActive(true);
        PlayerPrefs.SetInt("RunePoint",0);
    }

    public void FinishHurt()
    {
        _playerController.isHurt = false;
    }
}
