using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Play : MonoBehaviour
{
    public MainMenuBackgroundFade imageFadeIn;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        imageFadeIn.fadein = true;


    }
}
