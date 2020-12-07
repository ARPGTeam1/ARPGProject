using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Open_Anim : MonoBehaviour
{
    private float scale;
    private float scaler = 0.8f;
    bool done = false;

    
    private bool render => FramesDelay <= 0;
    public int FramesDelay = 0;

    void Start()
    {
        this.GetComponent<Image>().enabled = false;

    }

    void FixedUpdate()
    {
        this.GetComponent<Image>().enabled = render;
        FramesDelay--;
        if (FramesDelay <= 0) 
        {
            ScaleUp();
        } 
        
    }


    public void ScaleUp()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        if (!done)
        {
            scale = 1-scaler;
            scaler *= 0.9f;
            
            if (scaler < 0.001f) scaler = 0.000f;
            if (scale >= 1)
            {
                scale = 1;
                done = true;
                this.enabled = false;
            }
            
        }
    }


}
