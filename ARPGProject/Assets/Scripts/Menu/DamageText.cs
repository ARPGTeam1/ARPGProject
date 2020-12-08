using TMPro;
using UnityEngine;

namespace Menu
{
    public class DamageText : MonoBehaviour
    {
        private TextMeshPro _text;
        [SerializeField] private Vector3 offSet;
        [SerializeField] private float duration; 
        private Camera _mainCam;

        public void Setup(Transform location, int damage)
        {
            _text = GetComponent<TextMeshPro>();
            _text.SetText(damage.ToString());
            transform.position = location.position + offSet;
            Destroy(gameObject, duration);
        }
    
        private void Start()
        {
            _mainCam = Camera.main;
        }

        private void LateUpdate()
        {
            //put moving text here with code or animate?
            transform.LookAt(transform.position + _mainCam.transform.rotation * Vector3.forward, 
                                _mainCam.transform.rotation * Vector3.up);
        }
    }
}
