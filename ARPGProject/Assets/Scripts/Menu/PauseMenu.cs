﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused;
    public GameObject PauseMenuUI;
    public GameObject confirmationBox;
    public GameObject instructionPopup;
    public AudioSource audiosource;
    public AudioClip WOHClickSound;
    public AudioClip WOHMenuSoundtrack;

    private InstructionPopup _instruction;
    private SaveLoadScene _saveLoadScene;

    private void Start()
    {
        audiosource = GetComponentInChildren<AudioSource>();
        _saveLoadScene = new SaveLoadScene();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            OnClickSound();

            if (Paused)
            {
                Resume();
            }
            else
                OnPausePressed();
        }
    }

   public void Resume()
    {
        if (_instruction != null)
        {
            DestroyImmediate(_instruction.gameObject);
            ShowPausMenuUI();
        }else {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            Paused = false;
        }
    }
   
   public void OnPausePressed()
   {
       if (Paused) return;
       ShowPausMenuUI();
   }

   private void ShowPausMenuUI()
   {
       PauseMenuUI.SetActive(true);
       Pause();
   }

   void Pause()
    {
        Time.timeScale = 0f;
        Paused = true;
    }

    void MainMenu()
    {
        _saveLoadScene.SaveScene();
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
        instance.GetComponent<ConfirmationBox>().OnCancelled += ShowPausMenuUI;
    }
    
    public void OnQuitPressed()
    {
        PauseMenuUI.SetActive(false);
        var instance = Instantiate(confirmationBox, transform);
        instance.GetComponent<ConfirmationBox>().OnConfirmation += Quit;
        instance.GetComponent<ConfirmationBox>().OnCancelled += ShowPausMenuUI;
        
    }
    
    public void Quit()
    {
        _saveLoadScene.SaveScene();
        
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OnClickSound()
    {
        audiosource.PlayOneShot(WOHClickSound); 
    }
    private void OnDestroy()
    {
        confirmationBox.GetComponent<ConfirmationBox>().OnConfirmation -= Quit;
        confirmationBox.GetComponent<ConfirmationBox>().OnCancelled -= Resume;
    }
}
