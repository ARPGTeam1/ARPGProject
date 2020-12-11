using UnityEditor;
using UnityEngine;

namespace David_Test
{
    public class EditorTesting : EditorWindow
    {
        [MenuItem("Whispers of Hope/You're Breathtaking!!")]
        public static void BreathTaking()
        {
            Debug.LogError("Your'e Breathtaking!!");
        }
    }
}