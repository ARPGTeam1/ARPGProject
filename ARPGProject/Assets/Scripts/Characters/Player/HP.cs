using System;
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

        //Made this public to be able to modify reset currentHP when reviving
        
        public int CurrentHp
        {
            get => this._currentHp;
            set
            {
                this._currentHp = value;
                HPChanged.Invoke(value);
            }
        }
        
        /*
        idea : You could do something like this if you wanted to keep it private perhaps? :
        then calling heal() with no parameters is a full heal, and if specified will only heal that much. 
        
        public void Heal()
        {
            CurrentHp = maxHP;
        }
        
        public void Heal(int amount)
        {
            CurrentHp = Mathf.Clamp(CurrentHp + amount, 0, maxHP);
            //could clamp in CurrentHp setter instead, probably better.
        }
        */

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