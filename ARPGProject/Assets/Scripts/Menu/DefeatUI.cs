using Characters.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Menu
{
    public class DefeatUI : MonoBehaviour
    {
        public UnityEvent<string> DefeatUIText;
        private HP hitPoint;


        private void Start()
        {
            this.hitPoint = GetComponentInParent<HP>();
            this.hitPoint.BeenDefeatedText.AddListener(onDefeated);
            this.gameObject.SetActive(false);
        }

        private void onDefeated(string text)
        {
            this.DefeatUIText.Invoke(text);
            this.gameObject.SetActive(true);
        }
    }
}