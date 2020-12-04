using UnityEngine;
using Characters.Player;

public class MeleeEnemy : MonoBehaviour, IColliderListener
{

    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackWindupTime;
    [SerializeField] private float attackTimeCooldown;
    [SerializeField] private Collider collider;
    
    
    private GameObject _target;
    private HP _targetHpRef;
    private bool _isAttacking;
    //private float _elapsedTime;
    
    private float originalAttackCoolDown;
    
    public void Awake()
    {
        //_elapsedTime = 0f;
        originalAttackCoolDown = attackTimeCooldown;
    }

    private void Start()
    {
        // collider = GetComponentInChildren<SphereCollider>();
        if (collider.gameObject != gameObject)
        {
            ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
            cb.Initialize(this);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        _target = other.gameObject;
        if (_target != null)
        {
            _targetHpRef = _target.GetComponent<HP>();
            _isAttacking = true;
        }
    }

    private void Update()
    {
        if (attackTimeCooldown > 0)
        {
            
            attackTimeCooldown -= Time.deltaTime;
        }
        else
        {
            if (_isAttacking)
            {
                float distance = Vector3.Distance(this.transform.position, _target.transform.position);
                if (distance < attackRange)
                {
                    DamageTarget();
                    attackTimeCooldown = originalAttackCoolDown;

                    /*
                     * For when we have Enemy Attack animations? 
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

    private void DamageTarget()
    {
        _targetHpRef.TakeDamage(damage, this.name);
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (_target == null)
            return;
        _isAttacking = false;
        _target = null;
    }
}