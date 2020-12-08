using System;
using Menu;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemy
{
    public class Health : MonoBehaviour, IDamagable
    {
        [SerializeField] private bool spawnDamageText = true;
        [SerializeField] private GameObject damageTextPrefab;
        
        public int CurrentHealth
        {
            get => _currentHealth;
            private set => _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
        }
        private int _currentHealth;

        public int MaxHealth
        {
            get => maxHealth;
            private set => maxHealth = value;
        }
        [SerializeField] private int maxHealth;

        public event Action<int> OnHealthChanged;
        public event Action<int> OnDamaged;
        public UnityEvent onDeath;
        
        public bool IsDead => CurrentHealth <= 0;

        private void Start()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if(IsDead) return;
            
            OnHealthChanged?.Invoke(damage);
            OnDamaged?.Invoke(damage);
            if (spawnDamageText)
                DamageText(damage);
            CurrentHealth -= damage;
            if (IsDead)
            {
                onDeath.Invoke();
            }
        }

        public void Heal()
        {
            CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke(MaxHealth);
        }

        public void Heal(int amount)
        {
            CurrentHealth += amount;
            OnHealthChanged?.Invoke(amount);
        }

        public void DamageText(int damage)
        {
            Instantiate(damageTextPrefab).GetComponent<DamageText>().Setup(transform, damage);
        }

        public float HealthFillRatio() => CurrentHealth / MaxHealth;
    }
}
