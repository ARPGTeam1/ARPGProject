using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class DarkZone : MonoBehaviour
{
    
    public event Action OnDark;
    public event Action OnLight;
    
    [SerializeField] private Light sun;
    [SerializeField] private float darkness;
    [SerializeField] private float lightChangesPerTick = 0.05f;
    [SerializeField] private float delayBetweenTicks = 0.01f;
    private float startingLight;
    private void Start()
    {
        startingLight = sun.intensity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            StartCoroutine(nameof(Darken));
    }

    private IEnumerator Lighten()
    {
        Debug.Log("Leaving DarkZone");
        while (sun.intensity < startingLight)
        {
            sun.intensity += lightChangesPerTick;
            yield return new WaitForSeconds(delayBetweenTicks);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            StartCoroutine(nameof(Lighten));
    }

    private IEnumerator Darken()
    {
        Debug.Log("Entered DarkZone");
        while (sun.intensity > darkness)
        {
            sun.intensity -= lightChangesPerTick;
            yield return new WaitForSeconds(delayBetweenTicks);
        }
    }
}

