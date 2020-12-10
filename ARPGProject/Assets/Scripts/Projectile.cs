using System;
using UnityEngine;


public interface IProjectile
{
    void Spawn(GameObject target, GameObject owner);
}

public class Projectile : MonoBehaviour, IProjectile
{

    
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;
    [SerializeField] private bool shouldTrack;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject collisionEffect;
    
    private GameObject trackingTarget;
    private GameObject _owner;
    
    private bool Tracking => trackingTarget != null;
    
    
    public void Spawn(GameObject target, GameObject owner)
    {
        this._owner = owner;
        TrackTarget(target);
    }
    
    private void Update()
    {
        if (Tracking)
        {
            transform.LookAt(trackingTarget.transform.position + offset);
        }
        transform.Translate(Vector3.forward * (projectileSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == this._owner || other.gameObject.name == this.name)
        {
            return;
        }
        
        other.gameObject.GetComponent<IDamagable>()?.TakeDamage(this.projectileDamage, name);
        if(collisionEffect) Instantiate(collisionEffect, this.gameObject.transform.position, Quaternion.identity);
        
        Destroy(this.gameObject);
    }

    public void TrackTarget(GameObject target)
    {
        if (!shouldTrack)
        {
            return;
        }
        this.trackingTarget = target;
    }
}
