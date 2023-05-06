using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] private StoneDoor[] _stoneDoor;
    [SerializeField] private GameObject bossPanel;
    [SerializeField] private Image bossHpImage;
    private float maxBossHp;
    private EnemyHealth _enemyHealth;
    private PlayBGM bgm;

    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        maxBossHp = _enemyHealth.Health;
        bgm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<PlayBGM>();

    }

    private void Update()
    {

        if (_enemyHealth.Health <= 0)
        {
            for (int i = 0; i < _stoneDoor.Length; i++)
            {
                _stoneDoor[i].OpenDoor();
            }
            bgm.ChangeSong(SoundManager.SoundName.BGM2);
            bossPanel.SetActive(false);
            Destroy(gameObject);
        }

        if (_enemyHealth.Health > 0)
        {
            bossHpImage.fillAmount = _enemyHealth.Health / maxBossHp;
        }
        
        
        
    }
    
    
}
