using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Player;


public class MeleeEnemy : MonoBehaviour, IColliderListener
{

    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackTimeCooldown;
    
    private GameObject _target;
    private HP _targetHpRef;
    private bool _isAttacking;
    private float _elapsedTime;
    
    public void Awake()
    {
        Collider collider = GetComponentInChildren<SphereCollider>();
        if (collider.gameObject != gameObject)
        {
            ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
            cb.Initialize(this);
        }
    }

    private void Start()
    {
        _elapsedTime = 0f;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        _target = other.gameObject;
        if (_target != null)
        {
            _targetHpRef = _target.GetComponent<HP>();
            _isAttacking = true;
        }
    }

    private void Update()
    {
        if (_isAttacking)
        {
            float distance = Vector3.Distance(this.transform.position, _target.transform.position);
            if (distance < attackRange)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime > attackTimeCooldown)
                {
                    _elapsedTime -= attackTimeCooldown;
                    DamageTarget();
                }
            }
        }
    }

    private void DamageTarget()
    {
        _targetHpRef.TakeDamage(damage, this.name);
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (_target == null)
            return;
        _isAttacking = false;
        _target = null;
    }
}
