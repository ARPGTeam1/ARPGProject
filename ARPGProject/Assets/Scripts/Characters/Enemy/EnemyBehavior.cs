using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Enemy
{
    [RequireComponent(typeof(HealthManager))]
    public class EnemyBehavior : MonoBehaviour, IColliderListener, IKillable
    {

        [FormerlySerializedAs("collider")] [SerializeField] private Collider visionCollider;
        private GameObject _target;
        private HealthManager _targetHealth;
        private Vector3 _targetTransform;
        private MoveToTarget _moveToTarget;
        private Patrol _patrol;
        private MeleeEnemy _meleeEnemy;
        private RangedEnemy _rangedEnemy;
        private HealthManager _health;
        private Tracking _tracking;
        
        private bool HasTarget => _target != null;
        private bool CanMove => _moveToTarget != null;
        private bool CanPatrol => _patrol != null;
        private bool CanAttack => _meleeEnemy != null || _rangedEnemy != null;

        private bool CanTrack => _tracking != null;
        
        private bool IsDead => _health != null && _health.IsDead;



        public event Action OnDeath;

        public void Kill()
        {
            OnDeath?.Invoke();
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
            _tracking = this.gameObject.GetComponent<Tracking>();
            _health = GetComponent<HealthManager>();
        }
        
        private void Update()
        {
            if (IsDead)
            {
                if (CanAttack)
                {
                    if (_meleeEnemy && _rangedEnemy)
                    {
                        _meleeEnemy.enabled = false;
                        _rangedEnemy.enabled = false;
                    }
                    else if (_meleeEnemy && !_rangedEnemy) _meleeEnemy.enabled = false;
                    else if (!_meleeEnemy && _rangedEnemy) _rangedEnemy.enabled = false;
                }
                if (!CanPatrol) return;
                _patrol.enabled = false;
                if (!CanTrack) return;
                _tracking.enabled = false;
                return;
            }
            BehaviourTree();
        }


        private void BehaviourTree()
        {
            if (!HasTarget)
            {
                if (!CanPatrol) return;
                _patrol.enabled = true;
                return;
            }

            if (_targetHealth.IsDead)
            {
                _target = null;
                _targetHealth = null;
            }
            else
            {
                
                _targetTransform = new Vector3(_target.transform.position.x, this.transform.position.y, _target.transform.position.z);

                if (CanTrack)
                {
                    if (_tracking.checkFOV()) 
                        this.transform.LookAt(_targetTransform);
                }
                else 
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
                    
                    _moveToTarget.MoveTowards(this.gameObject, 0f);
                    return;
                }

                _patrol.enabled = false;
                _moveToTarget.MoveTowards(this.gameObject, 0f);
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
                        _moveToTarget.MoveTowards(_target,  _meleeEnemy.GetRange());
                        return;
                    }

                    _patrol.enabled = false;
                    _moveToTarget.MoveTowards(_target,  _meleeEnemy.GetRange());
                }    
            }
            else
            {
                if (CanMove && _moveToTarget.enabled)
                {
                    
                    if (!CanPatrol)
                    {
                        _moveToTarget.MoveTowards(this.gameObject, 0f);
                        return;
                    }

                    _patrol.enabled = false;
                    _moveToTarget.MoveTowards(this.gameObject, 0f);
                }
            }
        }

        private void MeleeOnly()
        {
            
            if (CanMove && _moveToTarget.enabled)
            {
                if (!CanPatrol)
                {
                    _moveToTarget.MoveTowards(_target,  _meleeEnemy.GetRange());
                    return;
                }

                _patrol.enabled = false;
                _moveToTarget.MoveTowards(_target, _meleeEnemy.GetRange());
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
            _targetHealth = _target.GetComponent<HealthManager>();
            
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
            _targetHealth = null;
            
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