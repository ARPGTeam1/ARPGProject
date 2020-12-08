using System;
using UnityEngine;

public class Weapon : MonoBehaviour, IPlayerDamage, ICollectable, IEquipable
{
    [SerializeField] private WeaponStats stats;

    public int GetDamage() => stats.damage;

    public void Equip()
    {
        throw new NotImplementedException();
    }

    public void Collect()
    {
        throw new NotImplementedException();
    }
    
    public override string ToString() => stats.name;
}

public interface IEquipable
{
    void Equip();
}

public interface IPlayerDamage
{
    int GetDamage();
}
