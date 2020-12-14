using System;
using Menu;
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
    //[SerializeField] private ParticleSystem _particleSystem;
    private Camera mainCam;
    public DefeatUI dUI;
    
    private void Start()
    {
        destinationText.SetText($"{prefixToDestName} {scene.name}");
        destinationText.enabled = false;
        mainCam = Camera.main;
        //_particleSystem.Stop();
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
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(this.dUI.FadeSquare());
            Invoke(nameof(InitializeScene), 2);
        }
    }

    private void InitializeScene() => SceneManager.LoadScene(this.scene.name);

    private void OnBecameVisible()
    {
        destinationText.enabled = true;
        // Debug.Log($"{name} became VISIBLE");
        //_particleSystem.Play();
    }

    private void OnBecameInvisible()
    {
        destinationText.enabled = false;
        // Debug.Log($"{name} became invisible");
        //_particleSystem.Stop();
    }
}
