using System;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Characters.Player
{
    public class Controller : MonoBehaviour
    {
        //Other script components
        [HideInInspector] public Combat combat;
        [HideInInspector] public Movement movement;
        [HideInInspector] public HealthManager health;
        
        [HideInInspector] public Camera mainCam;
        [HideInInspector] public Animator anim;

        public RaycastHit Hit;
        public LayerMask raycastLayers;

        private void Awake()
        {
            this.combat = GetComponent<Combat>();
            this.movement = GetComponent<Movement>();
            this.health = GetComponent<HealthManager>();
            this.anim = GetComponent<Animator>();

            this.mainCam = Camera.main;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
                MouseInput();
        }

        private void MouseInput()
        {
            if (this.health.IsDead) return;
            var ray = this.mainCam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out this.Hit, this.mainCam.farClipPlane, this.raycastLayers)) return;

            var hitLayer = this.Hit.transform.gameObject.layer;
            switch (hitLayer)
            {
                case 8: //Layer 8 = Ground Layer
                    this.combat.combatState = CombatState.Idle;
                    this.movement.MoveToMouse();
                    break;
                case 9: //Layer 9 = Targetable Layer
                    this.combat.combatState = CombatState.Attack;
                    break;
                default:
                    return;
            }
        }
    }
}
