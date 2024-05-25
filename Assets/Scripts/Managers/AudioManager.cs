using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class AudioManager : MonoBehaviour, IDataPersistance
{
    public static AudioManager Instance;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioSource SFXSource;

    [Header("Clips")]
    public AudioClip backgroundMusicClip;
    public AudioClip touchFlowerClip;
    public AudioClip buttonTouchClip;
    public AudioClip gameOverClip;
    public AudioClip obstacleHitClip;

    private List<AudioSource> audioSources = new List<AudioSource>();
    private float masterVolumeMusic = 1.0f;
    private float masterVolumeSfx = 0.7f;

    private bool musicEnabled;
    private bool sfxEnabled;

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

    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;

        if (enabled)
        {
            audioSource.volume = masterVolumeMusic;
        }
        else
        {
            audioSource.volume = 0f;
        }
    }

    public void SetSFXEnabled(bool enabled)
    {
        sfxEnabled = enabled;

        if (enabled)
        {
            SFXSource.volume = masterVolumeSfx;
        }
        else
        {
            SFXSource.volume = 0f;
        }
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

    public void LoadData(GameData data)
    {
        this.musicEnabled = data.musicEnabled;
        this.sfxEnabled = data.sfxEnabled;
    }

    public void SaveData(ref GameData data)
    {
        data.musicEnabled = musicEnabled;
        data.sfxEnabled = sfxEnabled;
    }
}
