using Characters.Player;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponStats stats;
    private float _lastAttackedTime;
    [SerializeField] private float damageInstanceDelay = 0.2f;
    private BoxCollider _collider;
    private Attack _attack;
    
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _attack = GetComponentInParent<Attack>();
    }

    private void Update()
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
    }

    private void DealDamage(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) 
            return;
        
        if (Time.time < _lastAttackedTime + damageInstanceDelay) 
            return;
        
        
        _lastAttackedTime = Time.time;
        other.gameObject.GetComponent<IDamagable>().TakeDamage(stats.damage, stats.weaponName);
    }

    public override string ToString() => stats.weaponName;
}


