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
    public AudioClip gameWonClip;

    private List<AudioSource> audioSources = new List<AudioSource>();
    private float masterVolumeMusic = 1.0f;
    private float masterVolumeSfx = 0.7f;
    private bool musicEnabled = true;
    private bool sfxEnabled = true;

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
        audioSource.clip = backgroundMusicClip;
    }

    public void PlayMusic()
    {
        if (!musicEnabled || audioSource.isPlaying)
        {
            return;
        }

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

    public void PlaySFX(AudioClipType clipType)
    {
        if (!sfxEnabled)
        {
            return;
        }

        if (SFXSource == null)
        {
            Debug.LogWarning("SFXSource is null");
        }

        switch (clipType)
        {
            case AudioClipType.ButtonTouched:
                SFXSource.PlayOneShot(buttonTouchClip);
                break;
            case AudioClipType.FlowerEntered:
                SFXSource.PlayOneShot(touchFlowerClip);
                break;
            case AudioClipType.GameOver:
                SFXSource.PlayOneShot(gameOverClip);
                break;
            case AudioClipType.ObstacleHit:
                SFXSource.PlayOneShot(obstacleHitClip);
                break;
            case AudioClipType.GameWon:
                SFXSource.PlayOneShot(gameWonClip);
                break;
            default:
                Debug.Log("Audio clip of type " + clipType + " does not exist");
                break;
        }
    }

    public void EnableOrDisableMusic(bool enabled)
    {
        if (enabled)
        {
            audioSource.volume = masterVolumeMusic;
            if (!musicEnabled)
            {
                musicEnabled = true;
                PlayMusic();
            }
        }
        else
        {
            audioSource.volume = 0f;
            if (musicEnabled)
            {
                musicEnabled = false;
                StopMusic();
            }
        }
    }

    public void EnableOrDisableSFX(bool enabled)
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

    public void ChangeMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void ResetVolumes() 
    {
        if(audioSource.volume != masterVolumeMusic)
        {
            audioSource.volume = masterVolumeMusic;
        }
        if(SFXSource.volume != masterVolumeSfx)
        {
            SFXSource.volume = masterVolumeSfx;
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
        musicEnabled = data.musicEnabled;
        sfxEnabled = data.sfxEnabled;
        PlayMusic();
    }

    public void SaveData(ref GameData data)
    {
        // NOTHING
    }
}
