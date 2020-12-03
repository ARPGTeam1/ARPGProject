using System;
using Characters.Player;
using UnityEngine;

public class StationaryThreatGround : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float startDamageTimerSeconds;
    [SerializeField] private float repeatDamageTimerSeconds;
    private HP target;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystem.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
                return;
        target = other.gameObject.GetComponent<HP>();
        if (target != null)
        {
            _particleSystem.Play();
            InvokeRepeating(nameof(DamageTarget), startDamageTimerSeconds, repeatDamageTimerSeconds);         
        }
    }

    private void DamageTarget()
    {
        target.TakeDamage(damage, "Player");
        Debug.Log($"Player taking {this.damage} damage");
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
        CancelInvoke(nameof(DamageTarget));
        _particleSystem.Stop();
    }
}