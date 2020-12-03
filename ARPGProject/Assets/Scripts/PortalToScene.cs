using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class PortalToScene : MonoBehaviour
{
    public Object scene;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(scene.name);
    }
}
