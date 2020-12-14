using System;
using Interfaces;
using Menu;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Characters.Enemy
{
    public class Health : MonoBehaviour, IDamagable, IKillable
    {
        [SerializeField] private bool spawnDamageText = true;
        [SerializeField] private GameObject damageTextPrefab;
        [HideInInspector] public UnityEvent<int> HPChanged;
        private Animator _animator;
        
        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
                HPChanged.Invoke(_currentHealth);
            } 
        }
        private int _currentHealth;

        public int MaxHealth
        {
            get => maxHealth;
            private set => maxHealth = value;
        }
        [SerializeField] private int maxHealth;

        public event Action OnHealthChanged;
        public event Action<int> OnDamaged;
        public event Action OnDeath;
        public UnityEvent onDeath;
        
        public bool IsDead => CurrentHealth <= 0;

        private void Start()
        {
            CurrentHealth = MaxHealth;
            this._animator = GetComponentInChildren<Animator>();
        }

        public void TakeDamage(int damage, string source)
        {
            if(IsDead) return;
            
            OnHealthChanged?.Invoke();
            OnDamaged?.Invoke(damage);
            this._animator.SetTrigger("GetHit");
            
            
            if (spawnDamageText)
                DamageText(damage);
            CurrentHealth -= damage;
            if (IsDead)
            {
                onDeath.Invoke();
            }
        }

        public void Kill()
        {
            OnDeath?.Invoke();
            
            // if (TryGetComponent<NavMeshAgent>(out var agent))
            //     agent.enabled = false;
            this._animator.SetTrigger("Die");
            Destroy(gameObject, 1);
        }

        public void Heal()
        {
            CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke();
        }

        public void Heal(int amount)
        {
            CurrentHealth += amount;
            OnHealthChanged?.Invoke();
        }

        public void DamageText(int damage)
        {
            Instantiate(damageTextPrefab).GetComponent<DamageText>().Setup(transform, damage);
        }

        public float HealthFillRatio() => CurrentHealth / MaxHealth;
    }
}
