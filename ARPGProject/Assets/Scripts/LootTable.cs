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

    public List<GameObject> RandomDrop(int amount)
    {
        List<GameObject> dropped = new List<GameObject>();
        
        if (amount > drops.Length) amount = drops.Length;

        for (int i = 0; i < amount; i++)
        {
            var drop = RandomDrop();
            if (dropped.Contains(drop))
            {
                i--;
                break;
            }
            else
            {
                dropped.Add(drop);
            }
        }
        return dropped;
    }
}
