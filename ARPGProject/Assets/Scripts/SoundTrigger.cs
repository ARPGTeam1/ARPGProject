using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip clip;

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
        if (played || !other.gameObject.CompareTag("Player")) return;
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
