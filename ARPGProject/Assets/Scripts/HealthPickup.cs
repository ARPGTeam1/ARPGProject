﻿using Characters;
using Characters.Player;
using Interfaces;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IConsumable
{

    [SerializeField] private int healAmount;
    [SerializeField] private GameObject pickUpEffect;

    public void Consume(Pandora pandora)
    {
        if(healAmount == 0)
            pandora.gameObject.GetComponent<HealthManager>().Heal();
        else
            pandora.gameObject.GetComponent<HealthManager>().Heal(healAmount);

        if(pickUpEffect != null)
            Instantiate(pickUpEffect, transform.position, this.gameObject.transform.rotation);

        Destroy(gameObject);
    }
}
