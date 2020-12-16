using System.Collections;
using Characters;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Menu
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private Vector3 offSet;
        [SerializeField] [Range(0f, 3f)] private float speed;
        [SerializeField] private float fadeMultiplier = 10f;
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private Color damageTextColor;
        
        private Camera _mainCam;
        
        public void Setup(Transform location, int damage)
        {
            if (location.gameObject.CompareTag("Player"))
                _text.color = Color.red;
            else
                _text.color = damageTextColor;
            
            _text.SetText(damage.ToString());
            transform.position = location.position + offSet;
            StartCoroutine(FadeAway());
        }

        private IEnumerator FadeAway()
        {
            while (_text.alpha > 0)
            {
                _text.alpha -= 0.1f * fadeMultiplier * Time.deltaTime;
                yield return null; 
            }
            Destroy(gameObject);
        }
        private void Start()
        {
            GameObject.FindWithTag("Player");
            _mainCam = Camera.main;
        }

        private void Update()
        {
           
            transform.Translate(Vector3.up * (speed * Time.deltaTime), Space.World);
            
        }

       
        private void LateUpdate() =>
            transform.LookAt(transform.position + _mainCam.transform.rotation * Vector3.forward, 
                                _mainCam.transform.rotation * Vector3.up);
    }
}
