using UnityEngine;

namespace Characters.Player
{
    public class TakeDamageTest : MonoBehaviour
    {
        private HealthManager hitPoint;

        private void Start()
        {
            hitPoint = GetComponent<HealthManager>();
        }

        public void TakeDamage()
        {
            hitPoint.TakeDamage(5,"Enemy");
        }
    }
}