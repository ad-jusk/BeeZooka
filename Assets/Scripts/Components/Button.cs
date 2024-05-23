using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private AudioManager audioManager;

    private void OnEnable()
    {
        //Because the ui canvas doesnt find it in the hierarchy otherwise
        StartCoroutine(DelayedInitialization());
    }

    private IEnumerator DelayedInitialization()
    {
        yield return null;

        audioManager = AudioManager.Instance;
    }
    public void PlayButtonTouchSound()
    {
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.buttonTouchClip);
        }
        else
        {
            Debug.LogError("AudioManager instance not found");
        }
    }
    public void MuteMusic()
    {
        if (audioManager != null)
        {
            audioManager.MuteMusic();
        }
    }
    public void MuteSFX()
    {
        if (audioManager != null)
        {
            audioManager.MuteSFX();
        }
    }

    public void UnmuteMusic()
    {
        if (audioManager != null)
        {
            audioManager.UnmuteMusic();
        }
    }
    public void UnmuteSFX()
    {
        if (audioManager != null)
        {
            audioManager.UnmuteSFX();
        }
    }
}
