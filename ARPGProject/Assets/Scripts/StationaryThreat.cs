using Characters.Player;
using UnityEngine;

public class StationaryThreat : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float startDamageTimerSeconds;
    [SerializeField] private float repeatDamageTimerSeconds;
    private HP target;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
                return;
        target = other.gameObject.GetComponent<HP>();
        if (target != null)
        {
            InvokeRepeating(nameof(DamageTarget), startDamageTimerSeconds, repeatDamageTimerSeconds);         
        }
    }

    private void DamageTarget()
    {
        target.TakeDamage(damage, this.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (target == null)
            return;
        CancelInvoke(nameof(DamageTarget));
        target = null;
    }
}