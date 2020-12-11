using System;
using System.Collections;
using Characters.Player;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponStats stats;
    private float _lastAttackedTime;
    private Collider _collider;
    private Attack _attack;
    
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _attack = GetComponent<Attack>();
    }

    private void Update()
    {
        _collider.enabled = _attack.IsAttacking;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) 
            return;
        
        other.gameObject.GetComponent<IDamagable>().TakeDamage(stats.damage, stats.weaponName);
        
    }

    public override string ToString() => stats.weaponName;
}


