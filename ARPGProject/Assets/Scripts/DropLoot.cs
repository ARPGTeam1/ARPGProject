using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    private enum DropType { RandomSingle, RandomMultiple, All, FirstInList  }
    
    [SerializeField] private LootTable lootTable;
    [SerializeField] private DropType dropType;
    [SerializeField] [Tooltip("This only affects RandomMultiple dropType")] private int amountOfDrops;
    [SerializeField] private int launchSpeed = 500;
    
    private void Start()
    {
        //Subscribe to OnDeathEvent of this object perhaps on hp class? 
    }

    void Drop()
    {
        switch (dropType)
        {
            case DropType.RandomSingle:
                var instance = Instantiate(lootTable.RandomDrop(), transform.position, Quaternion.identity);
                instance.GetComponent<Rigidbody>()?.AddForce(Vector3.up * launchSpeed);
                break;
            case DropType.RandomMultiple:
                foreach (var drop in lootTable.RandomDrop(amountOfDrops))
                {
                    var multipleInstance = Instantiate(drop, transform.position, Quaternion.identity);
                    multipleInstance.GetComponent<Rigidbody>()?.AddForce(Vector3.up * launchSpeed);
                }
                break;
            case DropType.All:
                foreach (var drop in lootTable.drops)
                {
                    var allInstance = Instantiate(drop, transform.position, Quaternion.identity);
                    allInstance.GetComponent<Rigidbody>()?.AddForce(Vector3.up * launchSpeed);
                }
                break;
            case DropType.FirstInList:
                var firstInstance = Instantiate(lootTable.drops[0], transform.position, Quaternion.identity);
                firstInstance.GetComponent<Rigidbody>()?.AddForce(Vector3.up * launchSpeed);
                firstInstance.GetComponent<Rigidbody>()?.AddTorque(firstInstance.transform.position - GameObject.FindWithTag("Player").transform.position);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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
