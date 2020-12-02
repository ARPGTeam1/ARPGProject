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
        [HideInInspector] public UnityEvent<string> BeenDefeatedText;
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
            isDefeat = false;
            this._agent = GetComponent<NavMeshAgent>();
        }

        public void TakeDamage(int amount, string source)
        {
            CurrentHp -= amount;
            if (CurrentHp <= 0 && !isDefeat)
            {
                ToBeDefeated(source);
            }
        }

        public void ToBeDefeated(string source)
        {
            isDefeat = true;
            this._agent.velocity = Vector3.zero;
            BeenDefeatedText.Invoke($"You are Defeated by {source}");
        }
    }
}