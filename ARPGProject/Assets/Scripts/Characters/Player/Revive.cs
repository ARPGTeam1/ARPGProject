using System;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Player
{
    public class Revive : MonoBehaviour
    {
        private HP _hp;
        private NavMeshAgent _agent;
        private AudioSource _source;

        private float _reviveDelay;

        private DefeatUI _dUI;
        public AudioClip clip;
        [HideInInspector] public Vector3 checkPoint = Vector3.zero;

        public Vector3 UpdateCheckpoint(Vector3 location)
        {
            return this.checkPoint = location;
        }

        private void Awake()
        {
            this._hp = GetComponent<HP>();
            this._agent = GetComponent<NavMeshAgent>();
            this._source = GetComponent<AudioSource>();
            this._dUI = FindObjectOfType<DefeatUI>();
            this._reviveDelay = this.clip.length;
        }
        

        public void CorpseRevive()
        {
            //?? Spawn protection? (Invisible/Invulnerable for X seconds)
            //?? Limited charges?
            //if button is pressed, heal the player to full
            //then regain control of the player character
            //make sure the player respawns in the same position they died in
            
            //also please fucking play the sound

            switch (this._hp.isDefeat)
            {
                case true:
                    this._source.PlayOneShot(this.clip);
                    this._dUI.gameObject.SetActive(false);
                    Invoke(nameof(ReviveLogic), this._reviveDelay);
                    break;
                case false:
                    Debug.LogError("Player isn't Dead. Please fix.");
                    break;
            }
        }

        private void ReviveLogic()
        {
            this._hp.CurrentHp = this._hp.maxHP;
            this._hp.isDefeat = false;
        }

        public void CheckpointRevive()
        {
            switch (this._hp.isDefeat)
            {
                case true:
                    this._agent.Warp(this.checkPoint);
                    this._source.PlayOneShot(this.clip);
                    this._dUI.gameObject.SetActive(false);
                    Invoke(nameof(ReviveLogic), this._reviveDelay);
                    break;
                case false:
                    Debug.LogError("Player isn't Dead. Please fix.");
                    break;
            }
        }
    }
}
