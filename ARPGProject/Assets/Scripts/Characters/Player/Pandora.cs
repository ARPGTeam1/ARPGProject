using Interfaces;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(HealthManager))]
    public class Pandora : MonoBehaviour
    {
        public Light hopeLight;
        private HealthManager _hp;
        
        [SerializeField] private float minLight;
        [SerializeField] private float maxLight;
        [Space]
        [SerializeField] private bool alsoBuffLightRadius;
        [SerializeField] private float minLightRadius;
        [SerializeField] private float maxLightRadius;

        private void Start()
        {
            _hp = GetComponent<HealthManager>();
            _hp.OnHealthChanged += UpdateLight;
            UpdateLight();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player")) return;
            
            other.gameObject.GetComponent<IConsumable>()?.Consume(this);
        }

        private void OnDestroy()
        {
            _hp.OnHealthChanged -= UpdateLight;
        }
        
        private void UpdateLight()
        {
            hopeLight.intensity = Mathf.Lerp(minLight, maxLight, _hp.HealthFillRatio());
            if(alsoBuffLightRadius)
                hopeLight.range = Mathf.Lerp(minLightRadius, maxLightRadius, _hp.HealthFillRatio());
        }

        public override string ToString() => $"Pandora's Light : {hopeLight.intensity}, : Radius : {hopeLight.range}";
    }
}
