using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToCredit : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private bool onPlayer;

    

    private void Update()
    {
        if (onPlayer)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }

        if (onPlayer && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.visible = true;
            SceneManager.LoadSceneAsync(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            onPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayer = false;
        }
    }
}
