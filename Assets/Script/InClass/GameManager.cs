using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Vector3 currentCheckPoint;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraPlayer;

    private void Start()
    {
        Cursor.visible = false;
        
        currentCheckPoint = 
            new Vector3(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"), PlayerPrefs.GetFloat("PositionZ"));

        player.position = new Vector3(currentCheckPoint.x,currentCheckPoint.y,currentCheckPoint.z);
        cameraPlayer.position = new Vector3(currentCheckPoint.x,currentCheckPoint.y,currentCheckPoint.z);
    }

    public void RestartGame()
    {
        Cursor.visible = false;
        SceneManager.LoadSceneAsync(1);
    }
}
