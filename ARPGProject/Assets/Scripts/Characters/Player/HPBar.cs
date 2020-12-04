using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Player
{

    public class HPBar : MonoBehaviour
    {
        private static Image HPBarImage;
        public UnityEvent<float> BarValueChanged;
        public UnityEvent<Color> BarColorChanged;
        public UnityEvent<string> BarTextChanged;
        private HP hitPoint;
        private void Start()
        {
            hitPoint = GetComponentInParent<HP>();
            HPBarImage = GetComponent<Image>();
            this.hitPoint.HPChanged.AddListener(OnHPChanged);
            OnHPChanged(hitPoint.maxHP);
        }

        private void OnHPChanged(int value)
        {
            var barValue = (float)value / hitPoint.maxHP;
            BarValueChanged.Invoke(barValue);
            BarColorChanged.Invoke(SetHPBarValue(barValue));
            BarTextChanged.Invoke($"{value} / {hitPoint.maxHP}");
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