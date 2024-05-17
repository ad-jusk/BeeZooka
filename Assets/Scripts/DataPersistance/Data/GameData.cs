
[System.Serializable]
public class GameData
{
    public bool musicEnabled;
    public int levelsCleared;

    public GameData() {
        this.musicEnabled = true;
        this.levelsCleared = 0;
    }
}
