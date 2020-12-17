using Characters.Player;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class PortalEnter : MonoBehaviour
    {
        private Camera _cam;
        private NavMeshAgent _agent;
        private GameObject player;
        private Movement stop;
        private float _stopDistance = 5;


        private void Awake()
        {
            _cam = Camera.main;
            player = GameObject.FindWithTag("Player");
            _agent = player.GetComponent<NavMeshAgent>();
        }

        private void LateUpdate()
        {
            // if (Input.GetMouseButtonDown(0)) MoveToPortal();
            //
            // if (Vector3.Distance(this.transform.position, this.player.transform.position) <= _stopDistance)
            // {
            //     this._agent.ResetPath();
            //     this._agent.velocity = Vector3.zero;
            //     this.player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            // }
        }

        private void MoveToPortal()
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, _cam.farClipPlane, LayerMask.GetMask("Portal")))
                _agent.destination = transform.position;
  
        }
    }
}