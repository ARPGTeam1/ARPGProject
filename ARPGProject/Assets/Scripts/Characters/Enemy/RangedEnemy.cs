using UnityEngine;

namespace Characters.Enemy
{
    public class RangedEnemy : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float attackMinRange;
        [SerializeField] private float attackMaxRange;
        [SerializeField] private float attackTimeCooldown;
        [SerializeField] private bool shouldShootSpawnProjectile;
        [SerializeField] private GameObject projectileToSpawn;
        [SerializeField] private GameObject projectileSpawnPoint;
        [SerializeField] [FMODUnity.EventRef] private string rangedAttackSound; 
    
        private Animator _animator;
        private GameObject _target;
        private HealthManager _targetHpRef;
        private float originalAttackCoolDown;
        private float _currentRange;
    
        private bool HasTarget => _target != null;
        public float AttackMinRange => attackMinRange;
        public float AttackMaxRange => attackMaxRange;
        public bool CanAttack => attackTimeCooldown <= 0;
    

        public void Awake()
        {
            originalAttackCoolDown = attackTimeCooldown;
            this._animator = GetComponentInChildren<Animator>();
        }
    
        void Update()
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
                    if (distance < attackMaxRange && distance > attackMinRange)
                    {
                        if (shouldShootSpawnProjectile)
                        {
                            var instance = Instantiate(projectileToSpawn, this.projectileSpawnPoint.transform.position, this.projectileSpawnPoint.transform.rotation);
                            instance.GetComponent<IProjectile>()?.Spawn(_target, this.gameObject);
                            this._animator.SetTrigger("Fire");
                            FMODUnity.RuntimeManager.PlayOneShotAttached(rangedAttackSound, gameObject);
                            attackTimeCooldown = originalAttackCoolDown;
                        }
                        else
                        {
                            this._animator.SetTrigger("Fire");
                            FMODUnity.RuntimeManager.PlayOneShotAttached(rangedAttackSound, gameObject);
                            DamageTarget();
                            attackTimeCooldown = originalAttackCoolDown;
                        }
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
            Debug.Log("Shooting player");
            _targetHpRef.TakeDamage(damage, this.name);
        }


        public float ReturnDistance()
        {
            if (!HasTarget)
            {
                return 0f;
            }
            _currentRange = Vector3.Distance(this.transform.position, _target.transform.position);
            return _currentRange;
        }
    }
}
