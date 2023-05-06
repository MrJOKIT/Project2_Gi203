using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointTotem : MonoBehaviour
{
    private bool haveSave;
    private bool onPlayer;

    [Header("Upgrade Totem")]
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject buttonPop;

    [Header("Upgrade Panel")] 
    [SerializeField] private Button upHpButton;
    [SerializeField] private int upHpCost;
    
    [SerializeField] private Button upSpButton;
    [SerializeField] private int upSpCost;
    
    [SerializeField] private Button upAtkButton;
    [SerializeField] private int upAtkCost;

    [Header("Ref")] 
    private PlayerCombat _playerCombat;
    private PlayerStats _playerStats;
    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (onPlayer && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.visible = true;
            upgradePanel.SetActive(true);
        }

        if (onPlayer)
        {
            buttonPop.SetActive(true);
        }
        else
        {
            buttonPop.SetActive(false);
        }

        if (_playerInventory.RunePoint < upHpCost)
        {
            upHpButton.interactable = false;
        }
        else
        {
            upHpButton.interactable = true;
        }
        
        if (_playerInventory.RunePoint < upSpCost)
        {
            upSpButton.interactable = false;
        }
        else
        {
            upSpButton.interactable = true;
        }
        
        if (_playerInventory.RunePoint < upAtkCost)
        {
            upAtkButton.interactable = false;
        }
        else
        {
            upAtkButton.interactable = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            onPlayer = true;
            
            
            var position = col.transform.position;
            PlayerPrefs.SetFloat("PositionX",position.x);
            PlayerPrefs.SetFloat("PositionY",position.y);
            PlayerPrefs.SetFloat("PositionZ",position.z);

            haveSave = true;
            PlayerPrefs.SetInt("HaveSave", haveSave?1:0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.visible = false;
            onPlayer = false;
            upgradePanel.SetActive(false);
        }
    }

    public void CloseUpgradePanel()
    {
        Cursor.visible = false;
        upgradePanel.SetActive(false);
    }

    public void UpgradeHealth()
    {
        _playerStats.MaxHealth += 10;
        PlayerPrefs.SetFloat("Health",_playerStats.Health);
        _playerInventory.RunePoint -= upHpCost;
    }
    
    public void UpgradeStamina()
    {
        _playerStats.MaxStamina += 10;
        PlayerPrefs.SetFloat("Stamina",_playerStats.Stamina);
        _playerInventory.RunePoint -= upSpCost;
    }
    
    public void UpgradeAttack()
    {
        _playerCombat.PlayerDamage += 10;
        PlayerPrefs.SetFloat("Atk",_playerCombat.PlayerDamage);
        _playerInventory.RunePoint -= upAtkCost;
    }
}
