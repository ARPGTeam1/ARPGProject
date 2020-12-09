using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Characters.Player
{
    public class HP : MonoBehaviour
    {
        public event Action OnDeath;
        public int maxHP;
        [HideInInspector] public bool isDefeat;
        private NavMeshAgent _agent;
        private Animator _animator;
        private PlayerAnimation _animation;
        private AudioSource _source;
        public AudioClip deathSound;
        [HideInInspector] public UnityEvent<int> HPChanged;
        [HideInInspector] public UnityEvent<string> BeenDefeatedText;
        private int _currentHp;
        
        public int CurrentHp
        {
            get => this._currentHp;
            private set
            {
                var clampValue = Mathf.Clamp(value, 0, maxHP);
                _currentHp = clampValue;
                HPChanged.Invoke(clampValue);
            }
        }

        public void Heal()
        {
            CurrentHp = maxHP;
        }

        public void Heal(int amount)
        {
            CurrentHp += amount;
        }

        private void Awake()
        {
            CurrentHp = maxHP;
            isDefeat = false;
            this._agent = GetComponent<NavMeshAgent>();
            this._animator = GetComponent<Animator>();
            this._source = GetComponent<AudioSource>();
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
            this._animator.SetBool("Dead", true);
            this._agent.velocity = Vector3.zero;
            this._agent.ResetPath();
            BeenDefeatedText.Invoke($"You are Defeated by {source}");
            this._source.PlayOneShot(this.deathSound);
            OnDeath?.Invoke();
        }
    }
}