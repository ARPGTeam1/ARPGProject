using System;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private Controller controller;
        
        public NavMeshAgent agent;
        private Camera _cam;
        private LayerMask _ground;
        private HealthManager _hitPoint;
        private Animator _animator;
        private Rigidbody _rb;
        private Attack _attack;
        
        [SerializeField] private GameObject clickDestinationPrefab;

        public bool HasEffect => clickDestinationPrefab != null;
        
        private void Awake()
        {
            this.controller = GetComponent<Controller>();
            
            this.agent = GetComponent<NavMeshAgent>();
            this._cam = Camera.main;
            this._ground = LayerMask.GetMask("Ground");
            this._hitPoint = GetComponent<HealthManager>();
            this._animator = GetComponent<Animator>();
            this._rb = GetComponent<Rigidbody>();
            this._attack = GetComponent<Attack>();
        }

        private void Update()
        {
            if (ShouldStop())
            {
                this.agent.ResetPath();
                this.agent.velocity = Vector3.zero;
                this._rb.angularVelocity = Vector3.zero;
            }

            UpdateAnimation();
        }
        
        public void MoveToMouse()
        {
            this.agent.ResetPath(); 
            this._rb.velocity = Vector3.zero;
            var instance = Instantiate(this.clickDestinationPrefab, this.controller.Hit.point, Quaternion.identity);
            this.agent.destination = this.controller.Hit.point;
            Destroy(instance, 1.5f);
        }
        
        private bool ShouldStop()
        {
            return Vector3.Distance(this.agent.destination, this.transform.position) <= 0f;
        }

        private void UpdateAnimation()
        {
            switch (this.agent.velocity.magnitude > 0)
            {
                case true:
                    this._animator.SetBool("isMoving", true);
                    break;
                case false:
                    this._animator.SetBool("isMoving", false);
                    break;
            }
        }
    }
}