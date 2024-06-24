using UnityEngine;
using UnityEngine.SceneManagement;

public class Beehive : MonoBehaviour, IDataPersistance
{
    private GameManager gameManager;
    private UiManager uiManager;
    private DataManager dataManager;
    private GameObject beehiveEffect;
    private bool levelCleared = false;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.OnFlowersCollected += HandleAllFlowersCollected;
        uiManager = UiManager.Instance;
        dataManager = DataManager.Instance;
        beehiveEffect = gameObject.transform.Find("hive_effect").gameObject;
    }

    private void FixedUpdate()
    {
        levelCleared = gameManager.AllFlowersCollected();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //levelCleared = gameManager.AllFlowersCollected();
        if (other.CompareTag("Bee"))
        {
            if (levelCleared)
            {
                gameManager.NotifyBeehiveEntered();
                uiManager.LevelCleared(gameManager.collectedCollectibles);
                Rigidbody2D beeRigidbody = other.GetComponent<Rigidbody2D>();
                beeRigidbody.velocity = Vector2.zero;
                beeRigidbody.position = transform.GetComponent<Renderer>().bounds.center;
                // SAVING DATA AFTER CLEARING LEVEL
                dataManager.SaveData();
            }
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

        int collectibleCount = gameManager.collectedCollectibles;
        if (data.levelIndexToCollectables.ContainsKey(currentLevel))
        {
            if (data.levelIndexToCollectables[currentLevel] < collectibleCount)
            {
                data.levelIndexToCollectables[currentLevel] = collectibleCount;
            }
        }
        else
        {
            data.levelIndexToCollectables.Add(currentLevel, collectibleCount);
        }
    }

    private void HandleAllFlowersCollected()
    {
        beehiveEffect.SetActive(true);
    }
}
