using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationBox : MonoBehaviour
{
    public Button yes;
    public Button no;

    public event Action OnConfirmation;
    public event Action OnCancelled;
    
    public AudioSource audiosource;
    public AudioClip WOHClickSound;
    
    public void Setup()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Confirm();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
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
        audiosource.PlayOneShot(WOHClickSound); 
    }
    
}
