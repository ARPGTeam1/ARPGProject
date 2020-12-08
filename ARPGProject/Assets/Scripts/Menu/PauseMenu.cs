using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused;
    public GameObject PauseMenuUI;
    public GameObject confirmationBox;
    public GameObject instructionPopup;
    public AudioClip WOHClickSound;
    public AudioClip WOHMenuSoundtrack;

    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.P)))

            if (Paused) 
                Resume();
            else
                OnPausePressed();
    }

   public void Resume()
    {
        PauseMenuUI.SetActive(false);
        instructionPopup.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        Paused = true;
    }
    
    

    void MainMenu()
    {
        Debug.Log("This will load the Main Menu");
    }
    
    public void SeeInstructions()
    {
        PauseMenuUI.SetActive(false);
        instructionPopup.SetActive(true);
    }

    public void OnPausePressed()
    {
        if (Paused) return;
        Pause();
        PauseMenuUI.SetActive(true);
    }

    public void OnQuitToMenuPressed()
    {
        PauseMenuUI.SetActive(false);
        var instance = Instantiate(confirmationBox, transform);
        instance.GetComponent<ConfirmationBox>().OnConfirmation += () =>
        {
            SceneManager.LoadScene(0);
        };
    }
    
    public void OnQuitPressed()
    {
        PauseMenuUI.SetActive(false);
        var instance = Instantiate(confirmationBox, transform);
        instance.GetComponent<ConfirmationBox>().OnConfirmation += Quit;
        instance.GetComponent<ConfirmationBox>().OnCancelled += Resume;
        
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

    private void OnDestroy()
    {
        confirmationBox.GetComponent<ConfirmationBox>().OnConfirmation -= Quit;
        confirmationBox.GetComponent<ConfirmationBox>().OnCancelled -= Resume;
    }
}
