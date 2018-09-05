using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class MainMenu : MonoBehaviour {

    public bool ismainmenu;


    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if(ismainmenu == true)
            QuitGame();
        }
    }
    public void PlaySinglePlayer()
    {
        Debug.Log("Loading Game...");
        GameManager.IsSinglePlayer = true;
        GameManager.IsOnline = false;
        SceneManager.LoadScene("SinglePlayer");
    }
    public void PlayMultiplayerPlayer()
    {
        Debug.Log("Loading Game...");
        GameManager.IsSinglePlayer = false;
        GameManager.IsOnline = false;
        SceneManager.LoadScene("MultiplayerLocal");
    }
    public void PlayOnline()
    {
        Debug.Log("Loading Online...");
        GameManager.IsSinglePlayer = false;
        GameManager.IsOnline = true; 
        SceneManager.LoadScene("MultiplayerOnline");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

    public void CreditsMenu()
    {
        Debug.Log("Loading Credits...");
    }
    public void OptionsMenu()
    {
        Debug.Log("Loading Options...");
    }
}
