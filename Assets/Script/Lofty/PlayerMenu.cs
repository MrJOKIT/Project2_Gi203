using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    private bool menuActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuActive)
        {
            Cursor.visible = true;
            menuActive = true;
            menuPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuActive)
        {
            Cursor.visible = false;
            menuActive = false;
            menuPanel.SetActive(false);
        }
    }

    public void CloseMenu()
    {
        Cursor.visible = false;
        menuActive = false;
        menuPanel.SetActive(false);
    }

    public void LoadScene(int sceneNumber)
    {
        Cursor.visible = false;
        SceneManager.LoadSceneAsync(sceneNumber);
    }

    public void MainMenu(int sceneNumber)
    {
        Cursor.visible = true;
        SceneManager.LoadSceneAsync(sceneNumber);
    }
}
