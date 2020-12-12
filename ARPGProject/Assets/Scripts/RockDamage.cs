using Interfaces;
using UnityEngine;

public class RockDamage : MonoBehaviour
{
    [SerializeField] private float velocityThreshold;
    [SerializeField] private int damage = 10;
    
    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        if(ShouldDealDamage())
            other.gameObject.GetComponent<IDamagable>()?.TakeDamage(damage, "Avalanche Rock");
    }
    
    private bool ShouldDealDamage() => Mathf.Abs(_rb.velocity.x) + Mathf.Abs(_rb.velocity.y) + Mathf.Abs(_rb.velocity.z) > velocityThreshold;
}
