using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFromBlack : MonoBehaviour
{
        
    private void Start()
    {
        var image = GetComponent<Image>();
        var tempcolor = image.color;
        tempcolor = new Color(0,0,0,1);
        image.color = tempcolor;
        
        
        var rect = GetComponent<RectTransform>();
    
        rect.offsetMax = Vector2.zero;
        rect.offsetMin = Vector2.zero;
    }

    void FixedUpdate()
    {
        var image = GetComponent<Image>();
        var tempcolor = image.color;
        
        tempcolor.a -= 0.05f;
        image.color = tempcolor;

        if (tempcolor.a <= 0)
        {
            DestroyImmediate(this);
        }
    }
}

