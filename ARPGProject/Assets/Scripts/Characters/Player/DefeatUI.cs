using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Characters.Player
{
    public class DefeatUI : MonoBehaviour
    {
        public UnityEvent<string> DefeatUIText;
        private HP hitPoint;
        public Vector3 checkPoint = Vector3.zero; //default
        public NavMeshAgent player;
        
        
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

        public void CorpseRevive()
        {
            //?? Spawn protection? (Invisible/Invulnerable for X seconds)
            //?? Limited charges?
            //if button is pressed, heal the player to full
            //then regain control of the player character
            //make sure the player respawns in the same position they died in

            switch (this.hitPoint.isDefeat)
            {
                case true:
                    this.hitPoint.CurrentHp = this.hitPoint.maxHP;
                    this.hitPoint.isDefeat = false;
                    this.gameObject.SetActive(false);
                    break;
                case false:
                    Debug.LogError("Player isn't Dead. Please fix.");
                    break;
            }
        }

        public void CheckpointRevive()
        {
            switch (this.hitPoint.isDefeat)
            {
                case true:
                    this.hitPoint.CurrentHp = this.hitPoint.maxHP;
                    this.hitPoint.isDefeat = false;
                    this.gameObject.SetActive(false);
                    this.player.Warp(checkPoint);
                    break;
                case false:
                    Debug.LogError("Player isn't Dead. Please fix.");
                    break;
            }
        }
    }
}