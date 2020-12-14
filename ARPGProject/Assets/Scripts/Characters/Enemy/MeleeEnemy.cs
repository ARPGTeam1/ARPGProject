using System;
using Characters.Player;
using UnityEngine;

namespace Characters.Enemy
{
    public class MeleeEnemy : MonoBehaviour
    {

        [SerializeField] private int damage;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackWindupTime;
        [SerializeField] private float attackTimeCooldown;
        
        private GameObject _target;
        private Animator _animator;
        private HP _targetHpRef;
        private float originalAttackCoolDown;
        //private float _elapsedTime;
        
        private bool HasTarget => _target != null;
        
        public bool CanAttack => attackTimeCooldown <= 0;
        
        public void Awake()
        {
            //_elapsedTime = 0f;
            originalAttackCoolDown = attackTimeCooldown;
            this._animator = GetComponentInChildren<Animator>();
        }

        private void Update()
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
                    if (distance < attackRange)
                    {
                        if (Vector3.Dot(this.transform.forward, _target.transform.position - this.transform.position) < 0)
                        {
                            return;
                        }
                        this._animator.SetTrigger("Melee");
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

        public float GetRange()
        {
            return this.attackRange;
        }
    }
}