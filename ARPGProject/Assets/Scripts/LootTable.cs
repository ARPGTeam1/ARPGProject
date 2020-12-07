using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot/New Loot Table")]
public class LootTable : ScriptableObject
{
    public GameObject[] drops;

    public GameObject RandomDrop()
    {
        var randomNum = Random.Range(0, drops.Length);
        return drops[randomNum];
    }

    public IEnumerable<GameObject> RandomDrop(int amount)
    {
        var dropped = new List<GameObject>();
        
        if (amount > drops.Length) amount = drops.Length;

        while (dropped.Count < amount)
        {
            var drop = RandomDrop();
            if (dropped.Contains(drop))
            {
               continue;
            }
            dropped.Add(drop);
            
        }
        return dropped;
    }
}
