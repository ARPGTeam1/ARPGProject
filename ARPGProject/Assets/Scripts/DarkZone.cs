using System;
using UnityEngine;
using UnityEngine.Events;

public class DarkZone : MonoBehaviour
{
    [SerializeField] private UnityEvent darkZoneEntered;
    [SerializeField] private UnityEvent darkZoneLeft;
    
    [SerializeField] private Light sun;
    [SerializeField] private float darkness;
    [SerializeField] private float lightChangesPerTick = 0.05f;
    [SerializeField] private float delayBetweenTicks = 0.01f;

    private enum Illumination { Light, Dark }
    private Illumination _lightState = Illumination.Light;
    private float _startingLight;
    private float _elapsed;
        
    private void Lighten() => sun.intensity += lightChangesPerTick;
    
    private void Darken() => sun.intensity -= lightChangesPerTick;
    
    private void Start() => _startingLight = sun.intensity;

    private void Update()
    {
        _elapsed += Time.deltaTime;
        
        if (_elapsed < delayBetweenTicks) return;
        
        switch (_lightState)
        {
            case Illumination.Dark:
                if (sun.intensity <= darkness) 
                    return;
                Darken();
                
                break;
            case Illumination.Light:
                if (sun.intensity >= _startingLight) 
                    return;
                Lighten();
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _elapsed -= delayBetweenTicks;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            _lightState = Illumination.Dark;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            _lightState = Illumination.Light;
    }
}

