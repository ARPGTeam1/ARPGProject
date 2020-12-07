using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int level;
    public int damage;
    public int maxHealth;
    public LootTable lootTable;
    public GameObject modelPrefab;
}
