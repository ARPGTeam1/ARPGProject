using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationBox : MonoBehaviour
{
    public Button yes;
    public Button no;

    public event Action OnConfirmation;
    public event Action OnCancelled;
    
    public 
    public AudioSource audiosource;
    public AudioClip onClickSound;
    
    public void Setup()
    {
        
    }

    void Start()
    {
        audiosource = GameObject.Find("PauseMenuAudioSource").GetComponent<AudioSource>();
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClickSound();
            Confirm();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickSound();
            Deny();
        }
    }

    public void Confirm()
    {
       OnConfirmation?.Invoke();
       Destroy(gameObject);
    }

    public void Deny()
    {
        
        OnCancelled?.Invoke();
        Destroy(gameObject);
    }
    
    public void OnClickSound()
    {
        audiosource.clip = onClickSound;
        audiosource.Play();
    }
    
}
