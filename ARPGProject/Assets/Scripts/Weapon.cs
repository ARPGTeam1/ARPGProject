using Interfaces;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponStats stats;
    private float _lastAttackedTime;
    
    public bool IsReady => Time.time > _lastAttackedTime + stats.attackCooldown;
        
    /*private void Update()
    {
        if (_attack.IsAttacking)
            _collider.enabled = true;

        _collider.enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        DealDamage(other);
    }

    private void OnCollisionStay(Collision other)
    {
        DealDamage(other);
    }*/
    private void Start()
    {
        _lastAttackedTime = -stats.attackCooldown;
    }

    public void DealDamage(IDamagable pTarget)
    {
        _lastAttackedTime = Time.time;
        pTarget?.TakeDamage(stats.damage, ToString());
        Debug.Log($"{stats.weaponName} damaged {pTarget} for {stats.damage}");
    }

    public override string ToString() => stats.weaponName;
}


