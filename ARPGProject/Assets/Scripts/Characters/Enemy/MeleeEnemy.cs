using UnityEngine;

namespace Characters.Enemy
{
    public class MeleeEnemy : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackTimeCooldown;
        [FMODUnity.EventRef] [SerializeField] private string meleeAttackSound; 
        
        private GameObject _target;
        private Animator _animator;
        private HealthManager _targetHpRef;
        private float originalAttackCoolDown;

        private bool HasTarget => _target != null;
        public bool CanAttack => attackTimeCooldown <= 0;
        
        public void Awake()
        {
            originalAttackCoolDown = attackTimeCooldown;
            this._animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (attackTimeCooldown > 0)
            {
                attackTimeCooldown -= Time.deltaTime;
            }
            else
            {
                if (HasTarget && !_targetHpRef.IsDead)
                {
                    float distance = Vector3.Distance(this.transform.position, _target.transform.position);
                    if (distance < attackRange)
                    {
                        if (Vector3.Dot(this.transform.forward, _target.transform.position - this.transform.position) < 0)
                        {
                            return;
                        }
                        this._animator.SetTrigger("Melee");
                        DamageTarget();
                        attackTimeCooldown = originalAttackCoolDown;
                    }
                }
            }
        }

        public void GetTarget(GameObject target)
        {
            this._target = target;
            this._targetHpRef = _target.GetComponent<HealthManager>();
        }
        
        public void ForgetTarget()
        {
            this._target = null;
            this._targetHpRef = null;
        }

        private void DamageTarget()
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(meleeAttackSound, gameObject);
            _targetHpRef.TakeDamage(damage, this.name);
        }

        public float GetRange() => this.attackRange;
    }
}