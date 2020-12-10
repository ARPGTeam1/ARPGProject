using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider))]
public class SoundTriggerAmbience : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") || _audioSource.isPlaying) return;
        PlayClip();
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(nameof(Fade));
    }

    IEnumerator Fade()
    {
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
        _audioSource.Stop();
    }

    private void PlayClip()
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

}
