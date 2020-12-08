using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider))]
public class SoundTrigger : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool onlyTriggerFirstTime;

    public bool played {
        get => PlayerPrefs.GetInt(clip.name, 0) != 0;
        set => PlayerPrefs.SetInt(clip.name, value ? 1 : 0);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        if (onlyTriggerFirstTime)
        {
            if (played) return;
            PlayClip();
        }
        else
        {
            if (_audioSource.isPlaying) return;
            PlayClip();
        }
    }

    private void PlayClip()
    {
        _audioSource.clip = clip;
        _audioSource.Play();
        played = true;
    }
    
    [ContextMenu("Clear Played Audio Queues")]
    public void ClearSongsPlayed()
    {
        PlayerPrefs.DeleteAll();
    }
}
