using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemy
{

    public class HealthBar : MonoBehaviour
    {
        private static Image HPBarImage;
        public UnityEvent<float> BarValueChanged;
        public UnityEvent<Color> BarColorChanged;
        public UnityEvent<string> BarTextChanged;
        private Health hitPoint;
        private void Start()
        {
            hitPoint = GetComponentInParent<Health>();
            HPBarImage = GetComponent<Image>();
            this.hitPoint.HPChanged.AddListener(OnHPChanged);
            OnHPChanged(hitPoint.MaxHealth);
        }

        private void OnHPChanged(int value)
        {
            var barValue = (float)value / hitPoint.MaxHealth;
            BarValueChanged.Invoke(barValue);
            BarColorChanged.Invoke(SetHPBarValue(barValue));
            BarTextChanged.Invoke($"{value} / {hitPoint.MaxHealth}");
        }

        public static Color SetHPBarValue(float value)
        {
            if(value < 0.2f)
            {
                return Color.red;
            }
            else if(value < 0.4f)
            {
                return Color.yellow;
            }
            else
            {
                return Color.green;
            }
        }

        private void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0,180,0);
        }
    }
}