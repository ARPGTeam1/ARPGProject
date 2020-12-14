using System;
using Menu;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Player
{
    public class Revive : MonoBehaviour
    {
        private HealthManager _hp;
        private NavMeshAgent _agent;
        private AudioSource _source;
        private Animator _animator;

        private float _reviveDelay;

        private DefeatUI _dUI;
        public GameObject defeatUI;
        public AudioClip clip;
        [HideInInspector] public Vector3 checkPoint = Vector3.zero;

        public void UpdateCheckpoint(Vector3 location)
        {
            this.checkPoint = location;
        }

        private void Awake()
        {
            this._hp = GetComponent<HealthManager>();
            this._animator = GetComponent<Animator>();
            this._agent = GetComponent<NavMeshAgent>();
            this._source = GetComponent<AudioSource>();
            this._dUI = FindObjectOfType<DefeatUI>();
            this._reviveDelay = this.clip.length;
        }
        

        public void CorpseRevive()
        {
            switch (this._hp.IsDead)
            {
                case true:
                    // this._source.PlayOneShot(this.clip);
                    this.defeatUI.SetActive(false);
                    this._animator.SetBool("Dead", false);
                    this._dUI.StartCoroutine(this._dUI.FadeSquare(false));
                    Invoke(nameof(ReviveLogic), 2f);
                    break;
                case false:
                    Debug.LogError("Player isn't Dead. Please fix.");
                    break;
            }
        }

        private void ReviveLogic()
        {
            this._hp.Heal();
            // this._hp.IsDead = false;
        }

        public void CheckpointRevive()
        {
            switch (this._hp.IsDead)
            {
                case true:
                    this._agent.Warp(this.checkPoint);
                    // this._source.PlayOneShot(this.clip);
                    this.defeatUI.SetActive(false);
                    this._animator.SetBool("Dead", false);
                    this._dUI.StartCoroutine(this._dUI.FadeSquare(false));
                    Invoke(nameof(ReviveLogic), 2f);
                    break;
                case false:
                    Debug.LogError("Player isn't Dead. Please fix.");
                    break;
            }
        }
    }
}
