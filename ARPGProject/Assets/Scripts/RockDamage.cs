using Characters.Player;
using UnityEngine;

public class RockDamage : MonoBehaviour
{
    [SerializeField] private float velocityThreshold;
    [SerializeField] private int _damage;
    
    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var rockSpeed = Mathf.Abs(_rb.velocity.x + _rb.velocity.z);
            if (rockSpeed > velocityThreshold)
            {
                other.gameObject.GetComponent<HP>().TakeDamage(_damage, "Avalanche Rock");
            }
            
        }
    }
}
