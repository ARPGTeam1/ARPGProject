using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Characters
{
    public class HealthBar : MonoBehaviour
    {
        private static Image HPBarImage;
        public UnityEvent<float> BarValueChanged;
        public UnityEvent<Color> BarColorChanged;
        public UnityEvent<string> BarTextChanged;
        private HealthManager hitPoint;
        [SerializeField] public GameObject player;

        private void Start()
        {
            hitPoint = (player != null) ? player.GetComponent<HealthManager>() :GetComponentInParent<HealthManager>();
            HPBarImage = GetComponent<Image>();
            hitPoint.HPChanged.AddListener(OnHPChanged);
            OnHPChanged(hitPoint.MaxHealth);
        }

        private void OnHPChanged(int value)
        {
            var barValue = (float) value / hitPoint.MaxHealth;
            BarValueChanged.Invoke(barValue);
            BarColorChanged.Invoke(SetHPBarValue(barValue));
            BarTextChanged.Invoke($"{value} / {hitPoint.MaxHealth}");
        }

        public static Color SetHPBarValue(float value)
        {
            // if (value < 0.2f)
            //     return Color.red;
            // if (value < 0.4f)
            //     return Color.yellow;
            // return Color.green;
            return Color.Lerp(Color.red, Color.green, value);
        }

        private void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}