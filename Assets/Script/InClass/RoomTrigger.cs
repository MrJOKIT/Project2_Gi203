using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private StoneDoor[] _doors;
    [SerializeField] private GameObject bossPanel;
    private bool doorActive;
    private PlayBGM bgm;

    private void Start()
    {
        bgm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<PlayBGM>();
    }


    private void Update()
    {
        if (doorActive)
        {
            for (int i = 0; i < _doors.Length; i++)
            {
                _doors[i].CloseDoor();
            }
            bgm.ChangeSong(SoundManager.SoundName.BGM3);
            bossPanel.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            doorActive = true;
        }
    }
}
