using UnityEngine;

namespace Characters.Player
{
    public class TakeDamageTest : MonoBehaviour
    {
        private HP hitPoint;

        private void Start()
        {
            hitPoint = GetComponent<HP>();
        }

        public void TakeDamage()
        {
            hitPoint.TakeDamage(5,"Enemy");
        }
    }
}