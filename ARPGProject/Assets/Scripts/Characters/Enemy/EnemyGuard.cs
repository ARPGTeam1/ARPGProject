using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyGuard : MonoBehaviour, IColliderListener
    {

        [SerializeField] private Collider collider;
        
        private GameObject target;

        public void Awake()
        {
            // Collider collider = GetComponentInChildren<SphereCollider>();
            // if (collider.gameObject != gameObject)
            // {
            //     ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
            //     cb.Initialize(this);
            // }
        }

        private void Start()
        {
            collider.gameObject.AddComponent<ColliderBridge>(); 
            collider.GetComponent<ColliderBridge>().Initialize(this);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
        }

        public void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
        }
    }
}