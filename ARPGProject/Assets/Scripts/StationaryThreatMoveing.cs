using System;
using Characters.Player;
using UnityEngine;

public class StationaryThreatMoveing : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float startDamageTimerSeconds;
    [SerializeField] private float repeatDamageTimerSeconds;
   
    private HP target;
    //[SerializeField] private GameObject moveToTarget;

    [SerializeField] private Vector3 moveToVector;
    [SerializeField] float projectileSpeed = 4f;
    
    [Range(0,1)] [SerializeField] float movementFactor; 
    
    Vector3 startingPos;

    private void Awake()
    {
        //moveToVector = moveToTarget.transform.TransformPoint(moveToTarget.transform.position);
    }

    void Start()
    {
        startingPos = transform.position;
    }
    private void Update()
    {
        if (projectileSpeed <= Mathf.Epsilon)
            return;
        
        float cycles = Time.time / projectileSpeed;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = moveToVector * movementFactor;
        transform.position = startingPos + offset;
    }

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
        target.TakeDamage(damage, "Player");
        Debug.Log($"Player taking {this.damage} damage");
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
        CancelInvoke(nameof(DamageTarget));
    }
}