using System;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class PortalToScene : MonoBehaviour
{
    [SerializeField] private TextMeshPro destinationText;
    [SerializeField] private string prefixToDestName;
    [SerializeField] string toSceneName;
    [SerializeField] private bool textFacesPlayer;
    //[SerializeField] private ParticleSystem _particleSystem;
    private Camera mainCam;
    public DefeatUI dUI;
    
    private void Start()
    {
        destinationText.SetText($"{prefixToDestName} {toSceneName}");
        destinationText.enabled = false;
        mainCam = Camera.main;
        //_particleSystem.Stop();
    }

    private void FixedUpdate()
    {
        destinationText.SetText($"{prefixToDestName} {toSceneName}");
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
            Invoke(nameof(InitializeScene), 2f);
        }
    }

    private void InitializeScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

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
