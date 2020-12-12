using Interfaces;
using UnityEngine;

namespace Characters.Player
{
    public class Attack : MonoBehaviour
    {
        private Camera _cam;
        private Weapon _weapon;
        private LayerMask _ground, _targetableLayerMask;
        private HP _health;
        private Animator _animator;
        [SerializeField] private GameObject lightning;
        [SerializeField] private string attackNameInAnimator; 
        [SerializeField] [Min(1.0f)] private float speedBuff = 1f;
        
        public bool IsAttacking => _animator.GetCurrentAnimatorStateInfo(0).IsName(attackNameInAnimator) && 
                                   _animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1;
        
        private void Start()
        {
            _cam = Camera.main;
            _weapon = transform.GetComponentInChildren<Weapon>();
            _health = GetComponent<HP>();
            this._ground = LayerMask.GetMask("Ground");
            this._targetableLayerMask = LayerMask.GetMask("Targetable");
            _animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            if (_health.isDefeat) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                if(!IsAttacking)
                    MeleeAttack();
                
            }

            if (Input.GetMouseButtonDown(1))
                RangedAttack();
        }

        private void MeleeAttack()
        {
            var ray = this._cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, this._cam.farClipPlane, _targetableLayerMask))
            {
                if(Vector3.Distance(transform.position,hit.transform.position) <= _weapon.stats.attackRange)
                {
                    if(_weapon.IsReady)
                    {
                        Debug.Log($"Attacking {hit.transform.gameObject.name}");
                        _animator.SetTrigger(attackNameInAnimator);
                        transform.LookAt(hit.transform);
                        
                        _weapon.DealDamage(hit.transform.GetComponent<IDamagable>());
                    }
                }
            }
        }

        private void RangedAttack()
        {
            var ray = this._cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, this._cam.farClipPlane, _ground))
            {
                //todo: think about system for equipping different skills
                //ISkill with a Range Property and void doSkill() method?
                //if(Vector3.Distance(transform.position,hit.transform.position) <= equippedSpellRange)
                DoSpell(hit.point);
            }
        }

        private void DoSpell(Vector3 castLocation)
        {
            Instantiate(lightning, castLocation, Quaternion.identity);
        }
    }
}
