using UnityEngine;

namespace Characters.Player
{
    public class Attack : MonoBehaviour
    {
        private Camera _cam;
        private Weapon _weapon;
        private LayerMask _ground;
        
        [SerializeField] private GameObject lightning; 
        
        private void Start()
        {
            _cam = Camera.main;
            _weapon = transform.GetComponentInChildren<Weapon>();
            this._ground = LayerMask.GetMask("Ground");

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = this._cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, this._cam.farClipPlane))
                {
                    if (hit.transform.gameObject.CompareTag("Enemy"))
                    {
                        if(Vector3.Distance(transform.position,hit.transform.position) <= _weapon.stats.attackRange)
                            SwingWeapon(hit.transform.GetComponent<IDamagable>());
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                var ray = this._cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, this._cam.farClipPlane, _ground))
                {
                    //if(Vector3.Distance(transform.position,hit.transform.position) <= _weapon.stats.attackRange)
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
            enemy?.TakeDamage(_weapon.stats.damage);
        }
        
        
    }
}
