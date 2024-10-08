using UnityEngine;

[DefaultExecutionOrder(-1)]
public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField]
    private string fileName;

    [SerializeField]
    private bool useEncryption;

    public static DataPersistanceManager Instance;
    public static GameData GameData;
    private static FileDataHandler dataHandler;
    private static object fileLock = new object();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        LoadGame();
    }

    private void NewGame()
    {
        GameData = new GameData();
    }

    private void LoadGame()
    {
        lock (fileLock)
        {
            GameData = dataHandler.Load();
            if (GameData == null)
            {
                Debug.Log("No game data was found, initialising default data");
                NewGame();
            }
            else
            {
                Debug.Log("Loaded data: " + GameData);
            }
        }

        LoadDataToDontDestroyOnLoadObjects();
    }

    /*
    LOADS DATA TO OBJECTS PRESENT IN EVERY SCENE, TO PREVENT THEM FROM BEING UPDATED
    EVERY TIME WE CHANGE SCENE
    */
    private void LoadDataToDontDestroyOnLoadObjects()
    {
        AudioManager.Instance.LoadData(GameData);
    }

    public static void SaveGame()
    {
        lock (fileLock)
        {
            Debug.Log("Saving data: " + GameData);
            dataHandler.Save(GameData);
        }
    }
}
