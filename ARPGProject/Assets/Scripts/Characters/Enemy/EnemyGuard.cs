using System;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyGuard : MonoBehaviour, IColliderListener
    {

        [SerializeField] private Collider collider;
        private GameObject target;
        private Vector3 targetTransform;

        public bool HasTarget => target != null;


        public void Awake()
        {
            // Collider collider = GetComponentInChildren<SphereCollider>();
            // if (collider.gameObject != gameObject)
            // {
            //     ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
            //     cb.Initialize(this);
            // }
        }

        private void Start()
        {
            collider.gameObject.AddComponent<ColliderBridge>(); 
            collider.GetComponent<ColliderBridge>().Initialize(this);
        }

        private void Update()
        {
            if (target)
            {
                targetTransform = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
                this.transform.LookAt(targetTransform);
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