using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class PortalToScene : MonoBehaviour
{
    public Object scene;
    [SerializeField] private TextMeshPro destinationText;
    [SerializeField] private string prefixToDestName;
    private void Start()
    {
        destinationText.SetText($"{prefixToDestName} {scene.name}");
        destinationText.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            destinationText.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            destinationText.enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(scene.name);
    }
}
