﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenFadeIn : MonoBehaviour
{
    public float fadeInRate = 0.05f;
    bool fadein = false;

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

            tempcolor.a += fadeInRate;
            image.color = tempcolor;

            if (tempcolor.a >= 1f)
            {
                tempcolor.a = 1f;
            }
        }
    }

    public void StartFadeIn()
    {
        fadein = true;
    }
}
