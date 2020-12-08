using UnityEngine;

namespace Characters.Player
{
    public class Attack : MonoBehaviour
    {
        private Camera _cam;
        private Weapon _weapon;
        
        private void Start()
        {
            _cam = Camera.main;
            _weapon = transform.GetComponentInChildren<Weapon>();

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = this._cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, this._cam.farClipPlane))
                {
                    if(Vector3.Distance(transform.position,hit.transform.position) <= _weapon.stats.attackRange)
                        SwingWeapon(hit.transform.GetComponent<IDamagable>());
                }
            }
        }

        private void SwingWeapon(IDamagable enemy)
        {
            enemy?.TakeDamage(_weapon.stats.damage);
        }
    }
}
