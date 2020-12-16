using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class PortalEnter : MonoBehaviour
    {
        private Camera _cam;
        private NavMeshAgent _agent;
        [SerializeField] public GameObject player;


        private void Awake()
        {
            _cam = Camera.main;
            player = GameObject.FindWithTag("Player");
            _agent = player.GetComponent<NavMeshAgent>();
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0)) MoveToPortal();
        }

        private void MoveToPortal()
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, _cam.farClipPlane, LayerMask.GetMask("Targetable")))
                _agent.destination = transform.position;
        }
    }
}