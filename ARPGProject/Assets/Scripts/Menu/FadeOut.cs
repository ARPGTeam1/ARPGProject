using System;
using System.Collections;
using UnityEngine;

namespace Menu
{
    public class FadeOut : MonoBehaviour
    {
        public GameObject blackSquare;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(FadeSquare());
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(FadeSquare(false));
            }
        }


        public IEnumerator FadeSquare(bool fadeToBlack = true, int fadeSpeed = 2)
        {
            Color objectColor = this.blackSquare.GetComponent<SpriteRenderer>().color;
            float fadeAmount;

            if (fadeToBlack)
            {
                while (this.blackSquare.GetComponent<SpriteRenderer>().color.a < 1)
                {
                    fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                    
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    this.blackSquare.GetComponent<SpriteRenderer>().color = objectColor;
                    yield return null;
                }
            }
            else
            {
                while (this.blackSquare.GetComponent<SpriteRenderer>().color.a > 0)
                {
                    fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                    
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    this.blackSquare.GetComponent<SpriteRenderer>().color = objectColor;
                    yield return null;
                }
            }
        }
    }
}
