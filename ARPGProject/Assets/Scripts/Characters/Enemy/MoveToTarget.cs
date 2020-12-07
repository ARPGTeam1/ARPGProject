using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    public class MoveToTarget : MonoBehaviour
    {
        
        private EnemyGuard _enemyGuard;
        private NavMeshAgent _agent;

        void Start()
        {
            _enemyGuard = GetComponentInParent<EnemyGuard>();
            _agent = GetComponentInParent<NavMeshAgent>();
        }

        public void MoveTowards(GameObject target)
        {
            float distance = Vector3.Distance(this.transform.position, target.transform.position);
            if (distance > 2f)
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
