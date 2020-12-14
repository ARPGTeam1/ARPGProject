using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    public class MoveToTarget : MonoBehaviour
    {
        private NavMeshAgent _agent;

        void Start()
        {
            _agent = GetComponentInParent<NavMeshAgent>();
        }

        public void MoveTowards(GameObject target, float attackRange)
        {
            float distance = Vector3.Distance(this.transform.position, target.transform.position);
            if (distance >= attackRange)
            {
                this._agent.SetDestination(target.transform.position);                
            }
            else
            {
                this._agent.SetDestination(this.transform.position);
            }
        }
    }
}
