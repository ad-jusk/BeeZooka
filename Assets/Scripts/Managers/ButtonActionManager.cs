using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ButtonActionManager : MonoBehaviour
{
    private AudioManager audioManager;

    private void OnEnable()
    {
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

    public void SetMusicEnabled(bool enabled)
    {
        if (audioManager != null)
        {
            audioManager.SetMusicEnabled(enabled);
        }
    }

    public void SetSFXEnabled(bool enabled)
    {
        if (audioManager != null)
        {
            audioManager.SetSFXEnabled(enabled);
        }
    }
}
