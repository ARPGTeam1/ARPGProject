using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponStats stats;
    private Vector3 offset;
    
    public override string ToString() => stats.name;
}


