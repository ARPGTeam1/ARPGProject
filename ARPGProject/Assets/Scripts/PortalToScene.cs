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
    [SerializeField] private bool textFacesPlayer;
    private Camera mainCam; 
    private void Start()
    {
        destinationText.SetText($"{prefixToDestName} {scene.name}");
        destinationText.enabled = false;
        mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        if (destinationText.enabled && textFacesPlayer)
        {
            destinationText.transform.LookAt(mainCam.transform);
            destinationText.transform.Rotate(Vector3.up * 180);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(scene.name);
    }

    private void OnBecameVisible()
    {
        destinationText.enabled = true;
        Debug.Log($"{name} became VISIBLE");
    }

    private void OnBecameInvisible()
    {
        destinationText.enabled = false;
        Debug.Log($"{name} became invisible");
    }
}
