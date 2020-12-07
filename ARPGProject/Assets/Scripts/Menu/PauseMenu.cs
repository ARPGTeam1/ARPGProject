using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused;
    public GameObject PauseMenuUI;
    public AudioClip WOHClickSound;
    public AudioClip WOHMenuSoundtrack;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.P)))

            if (Paused) 
                Resume();
            else
                Pause();
    }

   public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    void MainMenu()
    {
        Debug.Log("This will load the Main Menu");
    }
    
    void SeeInstructions()
    {
        
    }

    public void Quit()
    {
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
