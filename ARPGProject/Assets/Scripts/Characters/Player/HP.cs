using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Characters.Player
{
    public class HP : MonoBehaviour
    {
        public int maxHP;
        public bool isDefeat;
        private NavMeshAgent _agent;
        [HideInInspector] public UnityEvent<int> HPChanged;
        private int _currentHp;

        private int CurrentHp
        {
            get => _currentHp;
            set
            {
                _currentHp = value;
                HPChanged.Invoke(value);
            }
        }

        private void Awake()
        {
            CurrentHp = maxHP;
            this._agent = GetComponent<NavMeshAgent>();
        }

        public void TakeDamage(int amount, string source)
        {
            CurrentHp -= amount;
            if (CurrentHp <= 0 && !isDefeat)
            {
                ToBeDefeat(source);
            }
        }

        public void ToBeDefeat(string source)
        {
            isDefeat = true;
            this._agent.velocity = Vector3.zero;
        }
    }
}