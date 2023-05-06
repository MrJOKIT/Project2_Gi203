using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePoint : MonoBehaviour
{
    [SerializeField] private int runeTake;
    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerInventory>().TakeRune(runeTake);
            SoundManager.instace.Play(SoundManager.SoundName.Collect);
            Destroy(gameObject);
        }
    }
}
