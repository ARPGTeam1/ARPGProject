using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    
    [SerializeField] private int damage;
    [SerializeField] private float attackMinRange;
    [SerializeField] private float attackMaxRange;
    [SerializeField] private float attackWindupTime;
    [SerializeField] private float attackTimeCooldown;
    [SerializeField] private bool shouldShootSpawnProjectile;
    [SerializeField] private GameObject projectileToSpawn;
    [SerializeField] private GameObject projectileSpawnPoint;
    private Animator _animator;
    
    
    private GameObject _target;
    private HP _targetHpRef;
    private float originalAttackCoolDown;

    private bool HasTarget => _target != null;
    private float _currentRange;
    
    public float AttackMinRange
    {
        get => attackMinRange;
    }
    
    public float AttackMaxRange
    {
        get => attackMaxRange;
    }
    
    public bool CanAttack => attackTimeCooldown <= 0;
    

    public void Awake()
    {
        //_elapsedTime = 0f;
        originalAttackCoolDown = attackTimeCooldown;
        this._animator = GetComponentInChildren<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (attackTimeCooldown > 0)
        {
            
            attackTimeCooldown -= Time.deltaTime;
        }
        else
        {
            if (HasTarget && !_targetHpRef.isDefeat)
            {
                float distance = Vector3.Distance(this.transform.position, _target.transform.position);
                if (distance < attackMaxRange && distance > attackMinRange)
                {
                    //TODO: Spawn Projectile Prefab (perhaps has Tracking script on it?) and give it a target?
                    if (shouldShootSpawnProjectile)
                    {
                        var instance = Instantiate(projectileToSpawn, this.projectileSpawnPoint.transform.position, Quaternion.identity);
                        instance.GetComponent<IProjectile>()?.Spawn(_target, this.gameObject);
                        this._animator.SetTrigger("Fire");
                        attackTimeCooldown = originalAttackCoolDown;
                    }
                    else
                    {
                        DamageTarget();
                        attackTimeCooldown = originalAttackCoolDown;

                        /*
                        * ToDo: For when we have Enemy Attack animations? 
                        */
                        // _elapsedTime += Time.deltaTime; // Animation time ?
                        // if (_elapsedTime > attackWindupTime)
                        // {
                        //     _elapsedTime = 0;
                        //DamageTarget();
                        //}    
                    }
                }
            }
        }
    }
    
    public void GetTarget(GameObject target)
    {
        this._target = target;
        this._targetHpRef = _target.GetComponent<HP>();
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
