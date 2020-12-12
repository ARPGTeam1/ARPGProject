using System.Collections;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private Vector3 offSet;
        [SerializeField] [Range(0f, 3f)] private float speed;
        [SerializeField] private float fadeMultiplier = 10f;
        [SerializeField] private TextMeshPro _text;
        private Camera _mainCam;

        public void Setup(Transform location, int damage)
        {
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
        private void Start() => _mainCam = Camera.main;

        private void Update() =>
            transform.Translate(Vector3.up * (speed * Time.deltaTime), Space.World);

        private void LateUpdate() =>
            transform.LookAt(transform.position + _mainCam.transform.rotation * Vector3.forward, 
                                _mainCam.transform.rotation * Vector3.up);
    }
}
