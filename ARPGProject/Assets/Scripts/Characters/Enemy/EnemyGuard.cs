using System;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyGuard : MonoBehaviour, IColliderListener
    {

        [SerializeField] private Collider collider;
        private GameObject target;
        private Vector3 targetTransform;

        private bool HasTarget => target != null;
        private MoveToTarget _moveToTarget;

        public void Awake()
        {
   
        }

        private void Start()
        {
            collider.gameObject.AddComponent<ColliderBridge>(); 
            collider.GetComponent<ColliderBridge>().Initialize(this);
            _moveToTarget = this.gameObject.GetComponent<MoveToTarget>();
        }

        private void Update()
        {
            if (HasTarget)
            {
                targetTransform = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
                this.transform.LookAt(targetTransform);
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