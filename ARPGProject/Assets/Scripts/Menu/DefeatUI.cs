using System.Collections;
using Characters;
using UnityEngine;
using UnityEngine.Events;

namespace Menu
{
    public class DefeatUI : MonoBehaviour
    {
        public GameObject player;
        public GameObject defeatUI;
        public SpriteRenderer fadeSquare;
        public UnityEvent<string> DefeatUIText;
        private HealthManager hitPoint;


        private void Start()
        {
            this.hitPoint = this.player.GetComponent<HealthManager>();
            this.hitPoint.BeKilledBy.AddListener(onDefeated);
            this.defeatUI.SetActive(false);
        }

        private void onDefeated(string text)
        {
            this.DefeatUIText.Invoke(text);
            StartCoroutine(FadeSquare());
            Invoke(nameof(ActivateUI), 3);
        }

        private void ActivateUI() => this.defeatUI.SetActive(true);

        public IEnumerator FadeSquare(bool fadeToBlack = true, int fadeSpeed = 1)
        {
            Color objectColor = this.fadeSquare.color;
            float fadeAmount;

            if (fadeToBlack)
            {
                while (this.fadeSquare.color.a < 1)
                {
                    fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                    
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    this.fadeSquare.color = objectColor;
                    yield return null;
                }
            }
            else
            {
                while (this.fadeSquare.color.a > 0)
                {
                    fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                    
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    this.fadeSquare.color = objectColor;
                    yield return null;
                }
            }
        }
    }
}