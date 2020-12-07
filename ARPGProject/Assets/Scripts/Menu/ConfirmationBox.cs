using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationBox : MonoBehaviour
{
    public Button yes;
    public Button no;

    public event Action OnConfirmation;
    public event Action OnCancelled;
    
    public void Setup()
    {
        
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
}
