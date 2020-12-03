using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColliderListener
{
    void Awake();
    // {
    //     Collider collider = GetComponentInChildren<Collider>();
    //     if (collider.gameObject != gameObject)
    //     {
    //         ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
    //         cb.Initialize(this);
    //     }
    // }

    void OnTriggerEnter(Collider other);


    void OnTriggerExit(Collider other);
    // {
    //     if (!other.gameObject.CompareTag("Player"))
    //         return;
    //     Debug.Log("Bye bye, " + other.gameObject.tag + ".");
    // }
}
