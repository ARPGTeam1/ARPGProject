using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal myOtherPortal;
    [SerializeField] private Transform spawnLocation;

    private void Teleport(GameObject toTeleport)
    {
        var NavMesh = toTeleport.GetComponent<NavMeshAgent>();
        NavMesh.Warp(myOtherPortal.spawnLocation.transform.position);
        toTeleport.GetComponent<Rigidbody>().velocity = Vector3.zero;
        toTeleport.transform.rotation = myOtherPortal.spawnLocation.rotation;
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player"))
            Teleport(other.gameObject);
        
    }
}
