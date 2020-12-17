using System;
using Interfaces;
using Menu;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Characters
{
    public class HealthManager: MonoBehaviour, IDamagable, IKillable
    {
        [SerializeField] private bool spawnDamageText = false;
        [SerializeField] private GameObject damageTextPrefab;
        [HideInInspector] public UnityEvent<int> HPChanged;
        [HideInInspector] public UnityEvent<string> BeKilledBy;
        public event Action OnHealthChanged;
        public event Action<int> OnDamaged;
        public event Action OnDeath;
        public UnityEvent onDeath;
        private Animator _animator;
        
        private int _currentHealth;
        
        [SerializeField] private int maxHealth;
        
        [SerializeField] [FMODUnity.EventRef] private string DeathSound = "";

        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                var clampValue = Mathf.Clamp(value, 0, MaxHealth);
                _currentHealth = clampValue;
                HPChanged.Invoke(_currentHealth);
            }
        }
        
        public int MaxHealth
        {
            get => maxHealth;
            private set => maxHealth = value;
        }
        
        public bool IsDead => CurrentHealth <= 0;

        private void Awake()
        {
            CurrentHealth = MaxHealth;
            this._animator = GetComponentInChildren<Animator>();
        }

        public void TakeDamage(int damage, string source)
        {
            if (IsDead) return;

            OnHealthChanged?.Invoke();
            OnDamaged?.Invoke(damage);
            
            if (CompareTag("Enemy"))
            {
                _animator.SetTrigger("GetHit");
            }

            if (spawnDamageText && source != "StateManager") DamageText(damage);
            CurrentHealth -= damage;
            if (IsDead)
            {
                onDeath.Invoke();
                BeKilledBy.Invoke($"You are Defeated by {source}");
            }
        }

        public void Kill()
        {
            OnDeath?.Invoke();
            if (CompareTag("Player"))
            {
                _animator.SetBool("Dead", true);
            }

            if (CompareTag("Enemy"))
            {
                _animator.SetTrigger("Die");
                Destroy(gameObject, 1);
            }
            
            FMODUnity.RuntimeManager.PlayOneShotAttached(DeathSound, gameObject);
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

        public float HealthFillRatio()
        {
            return (float)CurrentHealth / MaxHealth;
        }
    }
}