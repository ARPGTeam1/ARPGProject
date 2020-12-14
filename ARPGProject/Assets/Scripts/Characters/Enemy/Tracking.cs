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
        private Vector3 targetPosition;
        [SerializeField] private Material[] lineMaterial;
        [SerializeField] private float stoppingDistance = 3f;

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
            line.materials = lineMaterial;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            _target = other.gameObject;
        }

        public void OnTriggerExit(Collider other)
        { 
            if (!other.gameObject.CompareTag("Player"))
                return;
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

        public bool checkFOV()
        {
            return fieldVision(_target);
        }

        private void Update()
        {
            
            if (fieldVision(_target))
            {
                //targetPosition = new Vector3(_target.transform.position.x,_target.transform.position.y + 1f,_target.transform.position.z);
                targetPosition = _target.transform.position + new Vector3(0, 2.5f, 0);
                Vector3 lineCastNormalizedY = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
                reachable = !Physics.Linecast(lineCastNormalizedY, targetPosition, out var hit, LayerMask.GetMask("Ground"));
                //agent.destination = reachable?_target.transform.position: this.transform.position;
                
                
                float distance = Vector3.Distance(this.transform.position, _target.transform.position);

                if (reachable)
                {
                    if (distance >= stoppingDistance)
                    {
                        this.agent.SetDestination(_target.transform.position);                
                    }
                    else
                    {
                        this.agent.SetDestination(this.transform.position);
                    }
                }
            }

            Drawline(reachable);
        }

        private void Drawline(bool reachable)
        {
            line.enabled = reachable;
            line.SetPosition(0, transform.position);
            if(reachable) line.SetPosition(1, targetPosition);
            line.startWidth = reachable ? 0.1f : 0f;
            line.endWidth = reachable ? 0.1f : 0f;
            line.startColor= Color.yellow;
            line.endColor= Color.red;
        }
    }
}