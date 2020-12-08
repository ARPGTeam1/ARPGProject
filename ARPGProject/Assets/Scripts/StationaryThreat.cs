using Characters.Player;
using UnityEngine;

public class StationaryThreat : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float startDamageTimerSeconds;
    [SerializeField] private float repeatDamageTimerSeconds;

    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;
    
    private HP _target;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
                return;
        _target = other.gameObject.GetComponent<HP>();
        if (_target != null)
        {
            InvokeRepeating(nameof(DamageTarget), startDamageTimerSeconds, repeatDamageTimerSeconds);         
        }
    }

    private void DamageTarget()
    {
        if (_audioClip != null)
        {
            if (!_target.isDefeat)
            {
                _audioSource.PlayOneShot(_audioClip);    
            }
        }
        _target.TakeDamage(damage, this.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_target == null)
            return;
        CancelInvoke(nameof(DamageTarget));
        _target = null;
    }
}