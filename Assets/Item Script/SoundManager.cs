using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip musicClip;
    public AudioClip jumpSound;
    public AudioClip pickupSound;
    public AudioClip dieSound;
    public AudioClip portalSound;
    public AudioClip runningSound;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }


    // Call this from other scripts: SoundManager.instance.PlaySFX(SoundManager.instance.jumpSound);
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);

        
    }
}