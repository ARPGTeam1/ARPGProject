using System;
using UnityEngine;

[Serializable]
public class DestructableObject : MonoBehaviour, IDamagable
{
    public event Action<GameObject, int> OnDamaged;
    
    private DropLoot _dropLoot;
    
    private void Start()
    {
        _dropLoot = GetComponent<DropLoot>();
    }

    public void TakeDamage(int damage)
    {
        OnDamaged?.Invoke(gameObject, damage);
        _dropLoot.Drop();
        Destroy(gameObject);    
    }
}