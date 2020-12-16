using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadScene 
{
    //private int setScene;

    public void SaveScene()
    {
        SavedScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadSavedScene()
    {
        SceneManager.LoadScene(SavedScene);
    }

    public int SavedScene
    {
        get => PlayerPrefs.GetInt("Scene", 1);  
        set => PlayerPrefs.SetInt("Scene", value);
    }
    
}
