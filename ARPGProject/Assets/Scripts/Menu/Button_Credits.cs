using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Credits : MonoBehaviour
{
    public GameObject CreditsScreen;
    public Transform Canvas;

    
    public void OpenCredits()
    {
        
    
        
        
        var instance = Instantiate(CreditsScreen);
        instance.transform.SetParent(Canvas);

        var rect = instance.GetComponent<RectTransform>();
    
        rect.offsetMax = Vector2.zero;
        rect.offsetMin = Vector2.zero;
        
    }
}
