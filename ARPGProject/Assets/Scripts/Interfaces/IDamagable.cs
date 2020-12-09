using System;
using UnityEngine;

public interface IDamagable
{
    event Action OnDeath;
    void TakeDamage(int damage, string souce);
    void Kill();
}
