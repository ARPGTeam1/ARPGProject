using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
   public void Quit()
    {

    #if UNITY_STANDALONE
    Application.Quit();
    #endif
            
    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #endif
        
    }

}
