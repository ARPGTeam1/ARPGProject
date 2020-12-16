using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
 
    private GameObject player;
    private bool initialHealth = true;


    private HealthManager healthRef;
    private int healthDiff;

    void Start()
    {   
        player = GameObject.FindWithTag("Player");
        healthRef = player.GetComponent<HealthManager>();
        //PlayerPrefs.SetInt("PlayerHealth", healthRef.MaxHealth);
        healthRef.Heal();
        
        // subscribe to sceneLoad event after First game startup, to get MaxHP-MaxHP = 0 for Updating Health
        SceneManager.sceneLoaded += CheckInitialHealth;
        
        // If has gone through a portal, set health to difference between MaxHealth and CurrentHealth, otherwise it is first time playing so do nothing
        
    }

    private void Update()
    {
        healthDiff = Mathf.Abs(healthRef.CurrentHealth - healthRef.MaxHealth);
    }

    public void CheckInitialHealth(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(healthDiff);
        player = GameObject.FindWithTag("Player");
        healthRef = player.GetComponent<HealthManager>();
        healthRef.TakeDamage(healthDiff, "StateManager");
    }
}
