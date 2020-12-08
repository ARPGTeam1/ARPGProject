﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreditScreen : MonoBehaviour
{
    private bool closeMenu = false;

 
    
    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (GetComponent<IsMouseOutsideThis>().IsPointerOutside())
            {
                closeMenu = true;
            }
            
            
        }
    }

    void FixedUpdate()
    {
        if (closeMenu) FadeAway();

    }

    void FadeAway()
    {

        var text = GetComponentsInChildren<Text>();
        var image = GetComponent<Image>();
        
        var tempcolor = image.color;
        var temptextcolor = text[0].color;
        
        tempcolor.a -= 0.1f;
        temptextcolor.a = tempcolor.a;
        
        foreach (var texts in text)
        {
            texts.color = temptextcolor;
        }
        image.color = tempcolor;
        
        
        if (tempcolor.a <= 0) 
        {
            DestroyImmediate(this.gameObject);
        }
    }
}