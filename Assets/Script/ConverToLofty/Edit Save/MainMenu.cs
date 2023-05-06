using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject optionPanel;
    private bool save = false;
    private bool haveSave;
    private bool menuActive = false;

    private void Start()
    {
        save = PlayerPrefs.GetInt("HaveSave")==1?true:false;
        if (save)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    private void Update()
    {
        if (menuActive && Input.GetKeyDown(KeyCode.Escape))
        {
            menuActive = false;
            optionPanel.SetActive(false);
        }
    }

    public void NewGame(int sceneNumber)
    {
        
        PlayerPrefs.SetFloat("PositionX",0);
        PlayerPrefs.SetFloat("PositionY",0);
        PlayerPrefs.SetFloat("PositionZ",0);

        PlayerPrefs.SetInt("RunePoint",0);
        
        PlayerPrefs.SetFloat("Health",100);
        PlayerPrefs.SetFloat("Stamina",100);
        PlayerPrefs.SetFloat("Atk",15);
        
        haveSave = false;
        PlayerPrefs.SetInt("HaveSave", haveSave?1:0);
        
        SceneManager.LoadSceneAsync(sceneNumber);
    }

    public void ContinueGame(int sceneNumber)
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }

    public void OpenOption()
    {
        menuActive = true;
        optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        menuActive = false;
        optionPanel.SetActive(false);
    }
}
