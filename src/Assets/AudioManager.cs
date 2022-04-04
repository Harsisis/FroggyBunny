using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] Forest;
    public AudioSource audioSource;
    private int musicIndex = 0;

    void Start()
    {
        audioSource.clip = Forest[0];
        audioSource.Play(0);
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % Forest.Length;
        audioSource.clip = Forest[musicIndex];
        audioSource.Play();
    }
}
