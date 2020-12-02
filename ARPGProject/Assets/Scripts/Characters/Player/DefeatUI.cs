using System;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Player
{
    public class DefeatUI : MonoBehaviour
    {
        public UnityEvent<string> DefeatUIText;
        private HP hitPoint;
        private void Start()
        {
            hitPoint = GetComponentInParent<HP>();
            hitPoint.BeenDefeatedText.AddListener(onDefeated);
            gameObject.SetActive(false);
        }

        private void onDefeated(string text)
        {
            DefeatUIText.Invoke(text);
            this.gameObject.SetActive(true);
        }
    }
}