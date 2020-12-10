using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;
    [SerializeField] private bool shouldTrack;
    [SerializeField] private Vector3 offset;
    
    private GameObject trackingTarget;
    
    
    private bool Tracking => trackingTarget != null;

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
        other.gameObject.GetComponent<IDamagable>()?.TakeDamage(this.projectileDamage, name);
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
