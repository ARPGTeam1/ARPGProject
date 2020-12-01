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

        private void Awake()
        {
            this._agent = GetComponent<NavMeshAgent>();
            this._cam = Camera.main;
            this._ground = LayerMask.GetMask("Ground");
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(1))
                MoveToLocation();
        }

        private void MoveToLocation()
        {
            var ray = this._cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, this._cam.farClipPlane, this._ground))
                this._agent.destination = hit.point;
        }
    }
}
