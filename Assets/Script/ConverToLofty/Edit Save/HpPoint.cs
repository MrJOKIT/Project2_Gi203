using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPoint : MonoBehaviour
{
    [SerializeField] private float healthTake;
    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerStats>().PlayerTakeHeath(healthTake);
            SoundManager.instace.Play(SoundManager.SoundName.Collect);
            Destroy(gameObject);
        }
    }
}
