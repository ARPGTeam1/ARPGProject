using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Weapon/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    //public Image icon;
    public string weaponName;
    public int damage;
    public float attackCooldown;
    public float attackRange;
    private Vector3 _offset;
    public GameObject model;
    public AudioClip hitSound;
}
