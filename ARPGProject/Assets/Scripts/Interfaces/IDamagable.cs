using System;
using UnityEngine;

public interface IDamagable
{
    event Action<GameObject, int> OnDamaged;
    void TakeDamage(int damage);
}
