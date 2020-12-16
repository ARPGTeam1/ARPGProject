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
    public bool fadein;
    private SaveLoadScene sceneToChangeTo;
    private void Start()
    {
        fadein = false;
        var image = GetComponent<Image>();
        var tempcolor = image.color;
        tempcolor = new Color(0,0,0,0);
        image.color = tempcolor;
        sceneToChangeTo = new SaveLoadScene();
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
                if (SceneManager.GetActiveScene().buildIndex != sceneToChangeTo.SavedScene)
                {
                    sceneToChangeTo.LoadSavedScene();
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                
            }
        }
    }
}

