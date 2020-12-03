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
        private HP hitPoint;
        private Animator _animator;

        private bool IsMoving()
        {
            return this._agent.velocity.magnitude > 0;
        }

        private void Awake()
        {
            this._agent = GetComponent<NavMeshAgent>();
            this._cam = Camera.main;
            this._ground = LayerMask.GetMask("Ground");
            hitPoint = GetComponent<HP>();
            this._animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
                MoveToLocation();

            if (this._agent.destination == this.transform.position)
            {
                this._agent.ResetPath();
            }

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

        private void MoveToLocation()
        {
            var ray = this._cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, this._cam.farClipPlane, this._ground) && !hitPoint.isDefeat)
                this._agent.destination = hit.point;
        }
    }
}