using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToScene : MonoBehaviour
{
    public Object scene;

    private void OnCollisionEnter(Collision other)
    {
        SceneManager.LoadScene(scene.name);
        
    }
}
