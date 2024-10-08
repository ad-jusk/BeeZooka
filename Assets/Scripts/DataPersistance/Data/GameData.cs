using System.Linq;

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
        string collectablesInfo = string.Join(", ", levelIndexToCollectables.Select(kvp => $"Level {kvp.Key}: {kvp.Value} collectibles"));

        return $"GameData: music enabled: {musicEnabled}, sfx enabled: {sfxEnabled}, levels cleared: {levelsCleared}, Collectibles per level: {(collectablesInfo.Length > 0 ? collectablesInfo : "None")}";
    }
}
