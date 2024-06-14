using UnityEngine;
using UnityEngine.SceneManagement;

public class Beehive : MonoBehaviour, IDataPersistance
{
    private GameManager gameManager;
    private UiManager uiManager;
    private DataManager dataManager;
    private bool levelCleared = false;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        uiManager = UiManager.Instance;
        dataManager = DataManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        levelCleared = gameManager.AllFlowersCollected();

        Debug.Log(levelCleared ? "LEVEL CLEARED" : "LEVEL FAILED");

        if (levelCleared)
        {
            uiManager.LevelCleared();
            Rigidbody2D beeRigidbody = other.GetComponent<Rigidbody2D>();
            beeRigidbody.velocity = Vector2.zero;
            beeRigidbody.position = transform.GetComponent<Renderer>().bounds.center;
            // SAVING DATA AFTER CLEARING LEVEL
            dataManager.SaveData();
        }
    }

    public void LoadData(GameData data)
    {
        // NOTHING
    }

    public void SaveData(ref GameData data)
    {
        int currentLevel = LevelIndexFromSceneExtractor.GetLevelIndex(SceneManager.GetActiveScene().name);

        if (levelCleared && currentLevel == data.levelsCleared + 1)
        {
            data.levelsCleared += 1;
        }

        int collectibleCount = GetCollectibleCountForCurrentLevel();
        if (data.levelIndexToCollectables.ContainsKey(currentLevel) && data.levelIndexToCollectables[currentLevel] < collectibleCount)
        {
            data.levelIndexToCollectables[currentLevel] = collectibleCount;
        }
        else
        {
            data.levelIndexToCollectables.Add(currentLevel, collectibleCount);
        }
    }
    private int GetCollectibleCountForCurrentLevel()
    {
        return gameManager.collectedCollectibles;
    }
}
