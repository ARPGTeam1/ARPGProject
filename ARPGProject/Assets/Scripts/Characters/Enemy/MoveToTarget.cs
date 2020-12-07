using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    public class MoveToTarget : MonoBehaviour
    {
        
        private EnemyGuard _enemyGuard;
        private NavMeshAgent _agent;
        
        // Start is called before the first frame update
        void Start()
        {
            _enemyGuard = GetComponentInParent<EnemyGuard>();
            _agent = GetComponentInParent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_enemyGuard.HasTarget)
            {
                
            }
        }
    }
}
