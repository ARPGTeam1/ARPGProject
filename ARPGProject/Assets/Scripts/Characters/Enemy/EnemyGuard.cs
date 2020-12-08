using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Enemy
{
    public class EnemyGuard : MonoBehaviour, IColliderListener
    {

        [FormerlySerializedAs("collider")] [SerializeField] private Collider visionCollider;
        private GameObject _target;
        private Vector3 _targetTransform;
        private MoveToTarget _moveToTarget;
        private Patrol _patrol;
        private MeleeEnemy _meleeEnemy;
        
        private bool HasTarget => _target != null;
        private bool CanMove => _moveToTarget != null;
        private bool CanPatrol => _patrol != null;

        private bool CanAttack => _meleeEnemy != null;

        public void Awake()
        {
   
        }
        
        private void Start()
        {
            visionCollider.gameObject.AddComponent<ColliderBridge>(); 
            visionCollider.GetComponent<ColliderBridge>().Initialize(this);
            _moveToTarget = this.gameObject.GetComponent<MoveToTarget>();
            _patrol = this.gameObject.GetComponent<Patrol>();
            _meleeEnemy = this.gameObject.GetComponent<MeleeEnemy>();
        }
        
        private void Update()
        {
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
            if (CanMove)
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

        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            _target = other.gameObject;
            
            if (CanAttack)
            {
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
                _meleeEnemy.ForgetTarget();   
            }
        }
    }
}