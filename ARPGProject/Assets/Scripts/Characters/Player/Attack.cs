using UnityEngine;

namespace Characters.Player
{
    public class Attack : MonoBehaviour
    {
        private Camera _cam;
        private Weapon _weapon;
        private LayerMask _ground;
        private HP _health;
        
        [SerializeField] private GameObject lightning; 
        
        private void Start()
        {
            _cam = Camera.main;
            _weapon = transform.GetComponentInChildren<Weapon>();
            _health = GetComponent<HP>();
            this._ground = LayerMask.GetMask("Ground");

        }
        
        private void Update()
        {
            
            if (_health.isDefeat) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                var ray = this._cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, this._cam.farClipPlane))
                {
                    if(Vector3.Distance(transform.position,hit.transform.position) <= _weapon.stats.attackRange)
                            SwingWeapon(hit.transform.GetComponent<IDamagable>());
                    
                }
            }

            if (Input.GetMouseButtonDown(1))
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
        }

        private void DoSpell(Vector3 castLocation)
        {
            Instantiate(lightning, castLocation, Quaternion.identity);
        }

        private void SwingWeapon(IDamagable enemy)
        {
            //todo: _anim.SetBool("");
            //todo: _weaponSwing sound?
                //var clip = _weapon.stats.hitSound;
                //But.. we will probably do this via fmod events im assuming
                //more info when Linn has done the homework.
            enemy?.TakeDamage(_weapon.stats.damage, this.name);
        }
    }
}
