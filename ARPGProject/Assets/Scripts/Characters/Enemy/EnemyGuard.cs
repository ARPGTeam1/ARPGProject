using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : MonoBehaviour, IColliderListener
{

    private GameObject target;

    
    public void Awake()
    {
        Collider collider = GetComponentInChildren<SphereCollider>();
        if (collider.gameObject != gameObject)
        {
            ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
            cb.Initialize(this);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        Debug.Log("I see you " + other.gameObject.tag + "!");
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        Debug.Log("Bye bye, " + other.gameObject.tag + ".");
    }
}
