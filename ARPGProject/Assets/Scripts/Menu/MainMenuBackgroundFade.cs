using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackgroundFade : MonoBehaviour
{

    public BlackScreenFadeIn blackscreen;
    public bool fadein = false;
    private float scaler = 1f;
    private bool fadeDone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
        var image = GetComponent<Image>();
        var tempcolor = image.color;
        
        
        if (fadein)
        {
            tempcolor.a += 0.05f;
            image.color = tempcolor;

            if (tempcolor.a >= 1f)
            {
                fadein = false;
                fadeDone = true;
                blackscreen.fadein = true;
            }
        }

        if (fadeDone)
        {
            this.transform.localScale *= scaler;

            scaler *= 1.00001f;
        }
        
    }
    
}
