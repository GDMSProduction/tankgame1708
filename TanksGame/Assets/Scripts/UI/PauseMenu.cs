using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenu : MonoBehaviour {

    public static bool IsGamePaused = false;
    public GameObject pauseMenuUI;
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("Cancel")|| Input.GetKey(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

     void Pause()
    {
        IsGamePaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void Resume()
    {
        IsGamePaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
