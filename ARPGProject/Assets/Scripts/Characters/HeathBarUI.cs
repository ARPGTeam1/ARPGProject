using System;
using System.Runtime.CompilerServices;
using Characters.Player;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(HealthBar))]
    public class HeathBarUI : MonoBehaviour
    {
        private void Start()
        {
            var player = GameObject.FindWithTag("Player");
            GetComponent<HealthBar>().player = player; ;
        }
    }
}