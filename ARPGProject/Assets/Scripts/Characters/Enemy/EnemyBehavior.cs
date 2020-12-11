using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Enemy
{
    [RequireComponent(typeof(Health))]
    public class EnemyBehavior : MonoBehaviour, IColliderListener, IKillable
    {

        [FormerlySerializedAs("collider")] [SerializeField] private Collider visionCollider;
        private GameObject _target;
        private Vector3 _targetTransform;
        private MoveToTarget _moveToTarget;
        private Patrol _patrol;
        private MeleeEnemy _meleeEnemy;
        private RangedEnemy _rangedEnemy;
        private Health _health;
        
        private bool HasTarget => _target != null;
        private bool CanMove => _moveToTarget != null;
        private bool CanPatrol => _patrol != null;
        private bool CanAttack => _meleeEnemy != null || _rangedEnemy != null;

        public void Awake()
        {
   
        }


        public void Kill()
        {
           Destroy(gameObject);
        }
        
        private void Start()
        {
            visionCollider.gameObject.AddComponent<ColliderBridge>(); 
            visionCollider.GetComponent<ColliderBridge>().Initialize(this);
            _moveToTarget = this.gameObject.GetComponent<MoveToTarget>();
            _patrol = this.gameObject.GetComponent<Patrol>();
            _meleeEnemy = this.gameObject.GetComponent<MeleeEnemy>();
            _rangedEnemy = this.gameObject.GetComponent<RangedEnemy>();
            _health = GetComponent<Health>();
        }
        
        private void Update()
        {
            if (_health.IsDead) return;
            BehaviourTree();
        }


        private void BehaviourTree()
        {
            if (!HasTarget)
            {
                if (!CanPatrol)
                {
                    return;
                }
                
                _patrol.enabled = true;
                return;
            }
            
            _targetTransform = new Vector3(_target.transform.position.x, this.transform.position.y, _target.transform.position.z);
            this.transform.LookAt(_targetTransform);

            if (CanAttack)
            {
             
                if (_rangedEnemy && _meleeEnemy)
                {
                    if (_meleeEnemy.CanAttack && _rangedEnemy.CanAttack)
                    {
                        RangedAndMelee();    
                    }
                }
                else if( _rangedEnemy && !_meleeEnemy)
                {
                    RangedOnly();
                }
                else if(_meleeEnemy && !_rangedEnemy)
                {
                    MeleeOnly();
                }
            }
        }

        private void RangedOnly()
        {
            
            if (DetermineDistance()) return;
            if (CanMove && _moveToTarget.enabled)
            {
                if (!CanPatrol)
                {
                    // TODO: Perhaps run away if only Ranged and target is too close?
                    // OR Only Ranged enemy has no minimum range?
                    
                    _moveToTarget.MoveTowards(this.gameObject);
                    return;
                }

                _patrol.enabled = false;
                _moveToTarget.MoveTowards(this.gameObject);
            }
        }

        private void RangedAndMelee()
        {

            if (DetermineDistance()) return;
            if (_rangedEnemy.ReturnDistance() <= _rangedEnemy.AttackMinRange)
            {
                
                if (CanMove && _moveToTarget.enabled)
                {
                    
                    if (!CanPatrol)
                   {
                        _moveToTarget.MoveTowards(_target);
                        return;
                    }

                    _patrol.enabled = false;
                    _moveToTarget.MoveTowards(_target);
                }    
            }
            else
            {
                if (CanMove && _moveToTarget.enabled)
                {
                    
                    if (!CanPatrol)
                    {
                        _moveToTarget.MoveTowards(this.gameObject);
                        return;
                    }

                    _patrol.enabled = false;
                    _moveToTarget.MoveTowards(this.gameObject);
                }
            }
        }

        private void MeleeOnly()
        {
            
            if (CanMove && _moveToTarget.enabled)
            {
                if (!CanPatrol)
                {
                    _moveToTarget.MoveTowards(_target);
                    return;
                }

                _patrol.enabled = false;
                _moveToTarget.MoveTowards(_target);
            }
        }

        private bool DetermineDistance()
        {
            if (_rangedEnemy.ReturnDistance() >= _rangedEnemy.AttackMinRange &&
                _rangedEnemy.ReturnDistance() <= _rangedEnemy.AttackMaxRange)
            {
                return true;
            }

            return false;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            _target = other.gameObject;
            
            if (CanAttack)
            {
                if(_rangedEnemy)
                    _rangedEnemy.GetTarget(_target);
                if(_meleeEnemy)
                    _meleeEnemy.GetTarget(_target);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            _target = null;
            
            if (CanAttack)
            {
                if (_rangedEnemy)
                    _rangedEnemy.ForgetTarget();
                if (_meleeEnemy)
                    _meleeEnemy.ForgetTarget();
            }
        }
    }
}