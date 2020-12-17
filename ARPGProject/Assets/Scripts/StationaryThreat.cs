using Characters;
using Interfaces;
using UnityEngine;

public class StationaryThreat : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float repeatDamageTimerSeconds;

    private float _lastDamaged;

    [FMODUnity.EventRef] [SerializeField] private string burnSound;

    private void OnTriggerEnter(Collider other)
    {
        DamageTarget(other.GetComponent<IDamagable>());
        _lastDamaged = Time.time;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!(Time.time >= _lastDamaged + repeatDamageTimerSeconds)) return;
        if (other.GetComponent<HealthManager>())
        {
            if(other.GetComponent<HealthManager>().IsDead) 
                return;
        }
        DamageTarget(other.GetComponent<IDamagable>());
    }

    private void DamageTarget(IDamagable target)
    {
        target?.TakeDamage(damage, this.name);
        _lastDamaged = Time.time;
        FMODUnity.RuntimeManager.PlayOneShot(burnSound, transform.position);
    }
}