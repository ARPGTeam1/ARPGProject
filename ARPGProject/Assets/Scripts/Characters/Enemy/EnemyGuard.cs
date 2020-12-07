using System;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyGuard : MonoBehaviour, IColliderListener
    {

        [SerializeField] private Collider collider;
        private GameObject target;
        private Vector3 targetTransform;
        private MoveToTarget _moveToTarget;
        private Patrol _patrol;
        
        private bool HasTarget => target != null;
        private bool CanMove => _moveToTarget != null;
        private bool CanPatrol => _patrol != null;

        public void Awake()
        {
   
        }
        
        private void Start()
        {
            collider.gameObject.AddComponent<ColliderBridge>(); 
            collider.GetComponent<ColliderBridge>().Initialize(this);
            _moveToTarget = this.gameObject.GetComponent<MoveToTarget>();
            _patrol = this.gameObject.GetComponent<Patrol>();
        }
        
        private void Update()
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
            targetTransform = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
            this.transform.LookAt(targetTransform);
            if (CanMove)
            {
                if (!CanPatrol)
                {
                    _moveToTarget.MoveTowards(target);
                    return;
                }

                _patrol.enabled = false;
                _moveToTarget.MoveTowards(target);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            target = other.gameObject;
        }

        public void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            target = null;
        }
    }
}