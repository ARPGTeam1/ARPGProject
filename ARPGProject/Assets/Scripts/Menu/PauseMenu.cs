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

    private InstructionPopup _instruction;

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
        if (_instruction != null)
        {
            DestroyImmediate(_instruction.gameObject);
            PauseMenuUI.SetActive(true);
        }else {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            Paused = false;
        }
    }
   
   public void OnPausePressed()
   {
       if (Paused) return;
       Pause();
       PauseMenuUI.SetActive(true);
   }

    void Pause()
    {
        Time.timeScale = 0f;
        Paused = true;
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void SeeInstructions()
    {
        PauseMenuUI.SetActive(false);
        _instruction = Instantiate(instructionPopup, transform).GetComponent<InstructionPopup>();
    }
    
    public void OnQuitToMenuPressed()
    {
        PauseMenuUI.SetActive(false);
        var instance = Instantiate(confirmationBox, transform);
        instance.GetComponent<ConfirmationBox>().OnConfirmation += MainMenu;
        instance.GetComponent<ConfirmationBox>().OnCancelled += Resume;
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
