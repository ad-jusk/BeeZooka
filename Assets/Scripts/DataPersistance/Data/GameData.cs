[System.Serializable]
public class GameData
{
    public bool musicEnabled;
    public bool sfxEnabled;
    public int levelsCleared;
    public SerializableDictionary<int, int> levelIndexToCollectables;

    public GameData()
    {
        this.musicEnabled = true;
        this.sfxEnabled = true;
        this.levelsCleared = 0;
        this.levelIndexToCollectables = new SerializableDictionary<int, int>();
    }

    public override string ToString()
    {
        return "GameData: music enabled: " + musicEnabled + ", sfx enabled: " + sfxEnabled + ", levels cleared: " + levelsCleared;
    }
}
