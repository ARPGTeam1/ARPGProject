using System;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    public class Tracking : MonoBehaviour, IColliderListener
    {
        private NavMeshAgent agent;
        private GameObject _target;
        public bool Escapeable;
        private bool reachable;
        private LineRenderer line;
        public float visionAngle;
        [SerializeField] private new Collider collider;

        public void Awake()
        {
        }
        
        void Start()
        {
            collider.gameObject.AddComponent<ColliderBridge>(); 
            collider.GetComponent<ColliderBridge>().Initialize(this);
            agent = GetComponent<NavMeshAgent>();
            this.gameObject.AddComponent<LineRenderer>(); 
            line = GetComponent<LineRenderer>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            _target = other.gameObject;
        }

        public void OnTriggerExit(Collider other)
        {
           if(Escapeable) _target = null;
           reachable = false;
           agent.destination = transform.position;
        }

        bool fieldVision(GameObject target)
        {
            if (_target != null)
            {
                Vector3 targetDir = target.transform.position - transform.position;
                float angle = Vector3.Angle(targetDir, transform.forward);
                return angle < visionAngle;
            }
            return false;
        }

        private void Update()
        {
            if (fieldVision(_target))
            {
                reachable = ! NavMesh.Raycast(transform.position, _target.transform.position, out NavMeshHit  hit, NavMesh.AllAreas);
                agent.destination = reachable?_target.transform.position: this.transform.position;
            }

            Drawline(reachable);
        }

        private void Drawline(bool reachable)
        {
            line.enabled = reachable;
            line.SetPosition(0, transform.position);
            if(reachable) line.SetPosition(1, _target.transform.position);
            line.startWidth = reachable ? 0.1f : 0f;
            line.endWidth = reachable ? 0.1f : 0f;
            line.startColor= Color.yellow;
            line.endColor= Color.red;
        }
    }
}