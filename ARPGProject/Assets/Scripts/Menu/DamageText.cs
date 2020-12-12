using System.Collections;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private Vector3 offSet;
        [SerializeField] private float duration;
        [SerializeField] [Range(0f, 1f)] private float speed ;
        
        [SerializeField] private TextMeshPro _text;
        private Camera _mainCam;

        public void Setup(Transform location, int damage)
        {
            _text.SetText(damage.ToString());
            transform.position = location.position + offSet;
            //StartCoroutine(FadeAway());
        }

        private IEnumerator FadeAway()
        {
            while (_text.alpha > 0)
            {
                _text.alpha--;
                yield return new WaitForSeconds(255 / (duration * 1000));
            }
            Destroy(gameObject);
        }
        private void Start()
        {
            _mainCam = Camera.main;
            //_text = GetComponent<TextMeshPro>();
            
        }

        private void Update()
        {
            transform.Translate(Vector3.up * (speed * Time.deltaTime), Space.World);
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _mainCam.transform.rotation * Vector3.forward, 
                                _mainCam.transform.rotation * Vector3.up);
        }
    }
}
