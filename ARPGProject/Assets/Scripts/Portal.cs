using System;
using System.Collections;
using Characters.Player;
using UnityEngine;
using UnityEngine.AI;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal myOtherPortal;
    [SerializeField] private Transform spawnLocation;

    private Revive _revive;

    private void Awake()
    {
        this._revive = FindObjectOfType<Revive>();
    }

    private void Teleport(GameObject toTeleport)
    {
        var NavMesh = toTeleport.GetComponent<NavMeshAgent>();
        NavMesh.Warp(myOtherPortal.spawnLocation.transform.position);
        toTeleport.GetComponent<Rigidbody>().velocity = Vector3.zero;
        toTeleport.transform.rotation = myOtherPortal.spawnLocation.rotation;
        this._revive.UpdateCheckpoint(this.myOtherPortal.spawnLocation.position);
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player"))
            Teleport(other.gameObject);
        
    }
}
