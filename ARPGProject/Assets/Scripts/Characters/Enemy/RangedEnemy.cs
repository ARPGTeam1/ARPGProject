﻿using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    
    [SerializeField] private int damage;
    [SerializeField] private float attackMinRange;
    [SerializeField] private float attackMaxRange;
    [SerializeField] private float attackWindupTime;
    [SerializeField] private float attackTimeCooldown;
    
    private GameObject _target;
    private HP _targetHpRef;
    private float originalAttackCoolDown;
    
    private bool HasTarget => _target != null;
    private float _currentRange;
    
    public float AttackMinRange
    {
        get => attackMinRange;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (attackTimeCooldown > 0)
        {
            
            attackTimeCooldown -= Time.deltaTime;
        }
        else
        {
            if (HasTarget && !_targetHpRef.isDefeat)
            {
                float distance = Vector3.Distance(this.transform.position, _target.transform.position);
                if (distance < attackMaxRange && distance > attackMinRange)
                {
                    DamageTarget();
                    attackTimeCooldown = originalAttackCoolDown;

                    /*
                    * ToDo: For when we have Enemy Attack animations? 
                    */
                    // _elapsedTime += Time.deltaTime; // Animation time ?
                    // if (_elapsedTime > attackWindupTime)
                    // {
                    //     _elapsedTime = 0;
                    //DamageTarget();
                    //}
                }
            }
        }
    }
    
    public void GetTarget(GameObject target)
    {
        this._target = target;
        this._targetHpRef = _target.GetComponent<HP>();
    }
        
    public void ForgetTarget()
    {
        this._target = null;
        this._targetHpRef = null;
    }
    
    private void DamageTarget()
    {
        _targetHpRef.TakeDamage(damage, this.name);
    }


    public float ReturnDistance()
    {
        if (!HasTarget)
        {
            return 0f;
        }
        _currentRange = Vector3.Distance(this.transform.position, _target.transform.position);
        return _currentRange;
    }
}