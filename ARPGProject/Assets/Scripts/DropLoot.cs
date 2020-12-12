using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropLoot : MonoBehaviour
{
    private enum DropType { RandomSingle, RandomMultiple, All, FirstInList  }
    
    [SerializeField] private LootTable lootTable;
    [SerializeField] private DropType dropType;
    [SerializeField] [Tooltip("This only affects RandomMultiple dropType")] private int amountOfDrops;
    [SerializeField] private int launchSpeed = 500;

    public void Drop()
    {
        switch (dropType)
        {
            case DropType.RandomSingle:
                var instance = Instantiate(lootTable.RandomDrop(), transform.position, Quaternion.identity);
                Launch(AddRigidbody(instance));
                break;
            
            case DropType.RandomMultiple:
                foreach (var drop in lootTable.RandomDrop(amountOfDrops))
                {
                    var multipleInstance = Instantiate(drop, transform.position, Quaternion.identity);
                    Launch(AddRigidbody(multipleInstance));
                }
                break;
            
            case DropType.All:
                foreach (var drop in lootTable.drops)
                {
                    var allInstance = Instantiate(drop, transform.position, Quaternion.identity);
                    Launch(AddRigidbody(allInstance));
                }
                break;
            
            case DropType.FirstInList:
                var firstInstance = Instantiate(lootTable.drops[0], transform.position, Quaternion.identity);
                Launch(AddRigidbody(firstInstance));
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private Rigidbody AddRigidbody(GameObject pObject) => pObject.GetComponent<Rigidbody>() ? 
        pObject.GetComponent<Rigidbody>() : pObject.AddComponent<Rigidbody>();

    private Vector3 LaunchDirection() => new Vector3(Random.Range(-1, 1), Random.Range(0, 1), Random.Range(-1,1));

    private void Launch(Rigidbody loot)
    {
        loot.AddForce(LaunchDirection() * launchSpeed);
        loot.AddTorque(loot.gameObject.transform.position - GameObject.FindWithTag("Player").transform.position);
    }
}
