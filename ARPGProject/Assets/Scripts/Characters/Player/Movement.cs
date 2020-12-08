using System;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Camera _cam;
        private LayerMask _ground;
        private HP _hitPoint;
        private Animator _animator;
        private Rigidbody _rb;

        private bool IsMoving()
        {
            return this._agent.velocity.magnitude > 0;
        }

        private void Awake()
        {
            this._agent = GetComponent<NavMeshAgent>();
            this._cam = Camera.main;
            this._ground = LayerMask.GetMask("Ground");
            this._hitPoint = GetComponent<HP>();
            this._animator = GetComponent<Animator>();
            this._rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
                MoveToMouse();

            if (ShouldStop())
            {
                this._agent.ResetPath();
                this._agent.velocity = Vector3.zero;
                this._rb.angularVelocity = Vector3.zero;
            }

            UpdateAnimation();
        }
        
        private void MoveToMouse()
        {
            this._agent.ResetPath(); 
            this._rb.velocity = Vector3.zero;
            var ray = this._cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, this._cam.farClipPlane, this._ground) && !this._hitPoint.isDefeat)
                this._agent.destination = hit.point;
        }
        
        private bool ShouldStop()
        {
            return Vector3.Distance(this._agent.destination, this.transform.position) <= 0f;
        }

        private void UpdateAnimation()
        {
            switch (this._agent.velocity.magnitude > 0)
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