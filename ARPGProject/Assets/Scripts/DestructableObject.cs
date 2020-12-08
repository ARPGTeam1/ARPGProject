using System;
using Characters.Enemy;
using UnityEngine;

[Serializable]
public class DestructableObject : MonoBehaviour
{
    private DropLoot _dropLoot;
    private Health health;
    private void Start()
    {
        _dropLoot = GetComponent<DropLoot>();
        health = GetComponent<Health>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        //GetComponent<IDamagable>().TakeDamage(other.gameObject.GetComponent<Player>().Damage());
        
        
        if(other.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(5);
            if(health.IsDead)
                Destroy(gameObject, 0.1f);
        }
        //if colliding with PlayerProjectile/weapon, (IPlayerDamageHandler?) 
        //get damageAmount from it (IPlayerDamageHandler.Damage)
        //deduct if from health with TakeDamage()
    }
}