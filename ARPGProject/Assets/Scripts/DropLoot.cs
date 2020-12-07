﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
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

    private Rigidbody AddRigidbody(GameObject pObject)
    {
        if (pObject.GetComponent<Rigidbody>()) return pObject.GetComponent<Rigidbody>();
        pObject.AddComponent<Rigidbody>();
        return pObject.GetComponent<Rigidbody>();
    }

    private Vector3 LaunchDirection()
    {
        return new Vector3(Random.Range(-1, 1), Random.Range(0, 1), Random.Range(-1,1));
    }

    private void Launch(Rigidbody gtfo)
    {
        gtfo.AddForce(LaunchDirection() * launchSpeed);
        gtfo.AddTorque(gtfo.gameObject.transform.position - GameObject.FindWithTag("Player").transform.position);
    }
    
    #region Testing_DELETE_ME
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Drop();
            Destroy(gameObject);
        }
    }

    #endregion
}
