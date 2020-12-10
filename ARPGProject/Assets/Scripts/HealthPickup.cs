using System;
using Characters.Player;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] private int healAmount;
    [SerializeField] private GameObject pickUpEffect;
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        if(healAmount == 0)
            other.gameObject.GetComponent<HP>().Heal();
        else
            other.gameObject.GetComponent<HP>().Heal(healAmount);

        if(pickUpEffect != null)
            Instantiate(pickUpEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);

    }
}
