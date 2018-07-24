using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class MainMenu : MonoBehaviour {
   

    public void PlaySinglePlayer()
    {
        Debug.Log("Loading Game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void PlayMultiplayerPlayer()
    {
        Debug.Log("Loading Game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
