using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class BlackScreenFadeInRoomChange : MonoBehaviour
{
    public float fadeInRate = 0.005f;
    public bool fadein = false;
    public Object sceneToChangeTo;
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }


        
}

