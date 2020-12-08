using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackScreenFadeIn : MonoBehaviour
{
    public bool fadein = false;

    private void Start()
    {
        var image = GetComponent<Image>();
        var tempcolor = image.color;
        tempcolor = new Color(0,0,0,0);
        image.color = tempcolor;
    }

    void FixedUpdate()
    {
        var image = GetComponent<Image>();
        var tempcolor = image.color;
        

        if (fadein)
        {

            tempcolor.a += 0.005f;
            image.color = tempcolor;

            if (tempcolor.a >= 1f)
            {
                SceneManager.LoadScene("Scene 1");
            }
        }
    }
        
        
}

