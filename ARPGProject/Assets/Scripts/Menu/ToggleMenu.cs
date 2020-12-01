using UnityEngine;

namespace Menu
{
    public class ToggleMenu : MonoBehaviour
    {
        public GameObject menu;
        public KeyCode toggleKey;
        
        private void Update()
        {
            if (Input.GetKeyDown(this.toggleKey))
                Toggle();
        }

        private void Toggle()
        {
            this.menu.SetActive(!this.menu.activeInHierarchy);
        }

        public void QuitGame()
        {
            #if UNITY_STANDALONE
            Application.Quit();
            #endif
            
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
