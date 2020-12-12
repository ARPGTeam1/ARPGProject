using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DestructableObject : MonoBehaviour, IKillable
{
    public UnityEvent onDestroyed;
    public event Action OnDeath;
    
    private LayerMask _groundMask;

    private void Start() => _groundMask = LayerMask.GetMask("Ground");

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == _groundMask) return;
        GetComponent<IDamagable>()?.TakeDamage(5, this.name);
        Kill();
    }
   
    public void Kill()
    {
        OnDeath?.Invoke();
        onDestroyed.Invoke();
        Destroy(gameObject);
    }
}