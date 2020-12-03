using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBridge : MonoBehaviour
{
    IColliderListener _listener;

    public void Initialize(IColliderListener l)
    {
        _listener = l;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _listener.OnTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        _listener.OnTriggerExit(other);
    }
}
