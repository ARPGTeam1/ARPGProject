﻿using System;
using FMODUnity;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Player
{
    public enum CombatState { Attack, Idle }
    [RequireComponent(typeof(NavMeshAgent))]
    public class Combat : MonoBehaviour
    {
        private Controller controller;
        
        public CombatState combatState;

        private RaycastHit _hit;
        private Weapon _equipped;

        private bool _attacking;
        
        private Transform _target;
        private HealthManager myHealth;

        private bool _targetIsDead => _target.GetComponent<HealthManager>().IsDead;
        private bool amIDead => myHealth.IsDead;

        [FMODUnity.EventRef] [SerializeField] private string attackSound;

        private void Start()
        {
            this.myHealth = gameObject.GetComponent<HealthManager>();
            this.controller = GetComponent<Controller>();
            this._equipped = GetComponentInChildren<Weapon>();
            
            this.combatState = CombatState.Idle;
        }

        private void Update()
        {
            switch (this.combatState)
            {
                case CombatState.Attack:
                    this._target = this.controller.Hit.transform;
                    if (_targetIsDead || amIDead)
                    {
                        this.combatState = CombatState.Idle;
                        return;
                    }
                    
                    if (Vector3.Distance(this._target.position, this.transform.position) > this._equipped.stats.attackRange +1f)
                    {
                        var enemyToPlayer = this.transform.position - this._target.position;
                        var steerTarget = this._target.position + enemyToPlayer.normalized * this._equipped.stats.attackRange;
                        this.controller.movement.agent.destination = steerTarget;
                         var lookDirection = new Vector3(this._target.position.x, this.transform.position.y, this._target.position.z);
                         this.transform.LookAt(lookDirection);
                    }
                    else
                    {
                        var lookDirection = new Vector3(this._target.position.x, this.transform.position.y, this._target.position.z);
                        this.transform.LookAt(lookDirection);
                        this.controller.anim.SetBool("Melee", true);
                    }
                    break;
                case CombatState.Idle:
                    this.controller.anim.SetBool("Melee", false);
                    break;
            }
        }

        public void SwordAttack() {
            this._equipped.DealDamage(this._target.GetComponent<IDamagable>());
            FMODUnity.RuntimeManager.PlayOneShotAttached(attackSound, _target.gameObject);
        }
    }
}
