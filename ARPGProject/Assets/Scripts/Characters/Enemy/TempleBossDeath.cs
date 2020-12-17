using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleBossDeath : MonoBehaviour
{
    [FMODUnity.EventRef] [SerializeField] private string bossDeathMonologue;

    public void PlayOnDeath()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(bossDeathMonologue, gameObject);
    }
}
