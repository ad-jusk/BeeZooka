using UnityEngine;

public class SoundEffectsButtonActionManager : ButtonActionManager, IDataPersistance
{
    private bool musicEnabled = true;
    private bool sfxEnabled = true;

    private GameObject musicOn;
    private GameObject musicOff;
    private GameObject sfxOn;
    private GameObject sfxOff;

    private DataManager dataManager;

    private new void OnEnable()
    {
        base.OnEnable();
        dataManager = DataManager.Instance;
        musicOn = GameObject.Find("MusicOn");
        musicOff = GameObject.Find("MusicOff");
        sfxOn = GameObject.Find("SFXOn");
        sfxOff = GameObject.Find("SFXOff");
    }

    public void LoadData(GameData data)
    {
        musicEnabled = data.musicEnabled;
        sfxEnabled = data.sfxEnabled;
        SetMusicButtonsActive();
        SetSfxButtonsActive();
    }

    public void SaveData(ref GameData data)
    {
        data.musicEnabled = musicEnabled;
        data.sfxEnabled = sfxEnabled;
    }

    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        SetMusicButtonsActive();
        audioManager.EnableOrDisableMusic(enabled);
        dataManager.SaveData();
    }

    public void SetSFXEnabled(bool enabled)
    {
        sfxEnabled = enabled;
        SetSfxButtonsActive();
        audioManager.EnableOrDisableSFX(enabled);
        dataManager.SaveData();
    }

    private void SetMusicButtonsActive()
    {
        musicOn.SetActive(musicEnabled);
        musicOff.SetActive(!musicEnabled);
    }

    private void SetSfxButtonsActive()
    {
        sfxOn.SetActive(sfxEnabled);
        sfxOff.SetActive(!sfxEnabled);
    }
}
