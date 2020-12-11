using System;
using UnityEngine;

namespace Characters.Player
{
    public class Pandora : MonoBehaviour
    {
        public Light hopeLight;
        private HP _hp;
        [SerializeField] private float minLight;
        [SerializeField] private float maxLight;
        [Space]
        [SerializeField] private bool alsoBuffLightRadius;
        [SerializeField] private float minLightRadius;
        [SerializeField] private float maxLightRadius;

        private void Start()
        {
            _hp = GetComponent<HP>();
            
        }

        public void ChangeLight(float lightIntensityBuffAmount = 0f, float lightRadiusBuffAmount = 0f)
        {
            hopeLight.intensity = Mathf.Clamp(hopeLight.intensity + lightIntensityBuffAmount, minLight, maxLight);
            if(alsoBuffLightRadius)
                hopeLight.range = Mathf.Clamp(hopeLight.range + lightRadiusBuffAmount, minLightRadius, maxLightRadius);

            Debug.Log(ToString());
        }

        public void sldfkjsdk()
        {
            hopeLight.intensity = Mathf.Lerp(minLight, maxLight, (float)_hp.CurrentHp / _hp.maxHP);
        }
        
        public override string ToString() => $"Pandora's Light : {hopeLight.intensity}, : Radius : {hopeLight.range}";
    }
}
