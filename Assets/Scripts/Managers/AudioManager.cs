using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Clips")]
    public AudioClip backgroundMusicClip;
    public AudioClip touchFlowerClip;
    public AudioClip buttonTouchClip;
    public AudioClip gameOverClip;
    public AudioClip obstacleHitClip;

    private List<AudioSource> audioSources = new List<AudioSource>();
    private float masterVolumeMusic = 1.0f; 
    private float masterVolumeSfx= 0.7f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        RegisterAudioSource(audioSource, masterVolumeMusic);
        RegisterAudioSource(SFXSource, masterVolumeSfx);
    }
    private void OnEnable()
    {
        audioSource.clip = backgroundMusicClip;
        PlayMusic();
    }
    public void PlayMusic()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("audioSource or audioClip is null.");
        }
    }
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        else
        {
            Debug.LogWarning("audioSource or audioClip is null.");
        }
    }
    public void PlaySFX(AudioClip audioClip)
    {
        if (SFXSource != null && audioClip != null)
        {
            SFXSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning("SFXSource or audioClip is null.");
        }
    }

    public void MuteMusic()
    {
        audioSource.volume = 0f;
    }
    public void MuteSFX()
    {
        SFXSource.volume = 0f;
    }

    public void UnmuteMusic()
    {
        audioSource.volume = masterVolumeMusic;
    }

    public void UnmuteSFX()
    {
        SFXSource.volume = masterVolumeSfx;
    }

    private void RegisterAudioSource(AudioSource source, float masterVolume)
    {
        if (source != null && !audioSources.Contains(source))
        {
            audioSources.Add(source);
            source.volume = masterVolume;
        }
        else
        {
            Debug.LogWarning("AudioSource is null or already registered.");
        }
    }

}
