[System.Serializable]
public class GameData
{
    public bool musicEnabled;
    public bool sfxEnabled;
    public int levelsCleared;

    public GameData()
    {
        this.musicEnabled = true;
        this.sfxEnabled = true;
        this.levelsCleared = 0;
    }

    public override string ToString()
    {
        return "GameData: music enabled: " + musicEnabled + ", sfx enabled: " + sfxEnabled + ", levels cleared: " + levelsCleared;
    }
}
