using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ButtonActionManager : MonoBehaviour
{
    protected AudioManager audioManager;

    protected void OnEnable()
    {
        audioManager = AudioManager.Instance;
    }

    public void PlayButtonTouchSound()
    {
        if (audioManager != null)
        {
            audioManager.PlaySFX(AudioClipType.ButtonTouched);
        }
        else
        {
            Debug.LogError("AudioManager instance not found");
        }
    }
}
