using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : Singleton<GameManager>, IDataPersistance
{
    #region GameEvents

    public delegate void BeehiveEntered();
    public event BeehiveEntered OnBeehiveEntered;
    public delegate void BeehiveMissed();
    public event BeehiveMissed OnBeehiveMissed;
    public delegate void ObstacleEntered();
    public event ObstacleEntered OnObstacleEntered;
    public delegate void PauseButtonClicked();
    public event PauseButtonClicked OnPauseButtonClicked;
    public delegate void ResumeButtonClicked();
    public event ResumeButtonClicked OnResumeButtonClicked;
    public delegate void RestartButtonClicked();
    public event RestartButtonClicked OnRestartButtonClicked;

    public delegate void FlowerEntered(FlowerColor flowerColor);
    public event FlowerEntered OnFlowerEntered;

    [SerializeField] public GameObject WinMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject lostMenu;
    #endregion
    //private GameData gameData;


    #region  GameStats

    public List<FlowerColor> flowersToCollect;
    public List<FlowerColor> collectedFlowers;

    #endregion

    private void Awake() {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Level1":
                flowersToCollect = new() { FlowerColor.RED, FlowerColor.PINK };
                break;

        }
        collectedFlowers = new();
    }

    public void NotifyBeehiveEntered() {
        OnBeehiveEntered?.Invoke();
    }

    public void NotifyBeehiveMissed() {
        OnBeehiveMissed?.Invoke();
        lostMenu.SetActive(true);
    }
    public void NotifyObstacleEntered()
    {
        OnObstacleEntered?.Invoke();
    }
    public void NotifyPauseButtonClicked()
    {
        OnPauseButtonClicked?.Invoke();
        pauseMenu.SetActive(true);
    }
    public void NotifyResumeButtonClicked()
    {
        OnResumeButtonClicked?.Invoke();
        pauseMenu.SetActive(false);
    }
    public void NotifyRestartButtonClicked()
    {
        OnRestartButtonClicked?.Invoke();
        //LoadData(gameData);
        lostMenu.SetActive(false);
        pauseMenu.SetActive(false);
        WinMenu.SetActive(false);
    }

    public void NotifyFlowerEntered(FlowerColor flowerColor) {
        collectedFlowers.Add(flowerColor);
        OnFlowerEntered?.Invoke(flowerColor);
    }

    public bool AllFlowersCollected() {

        if (flowersToCollect.Count != collectedFlowers.Count){
            return false;
        }

        var itemCounts = new Dictionary<FlowerColor, int>();

        foreach (FlowerColor s in flowersToCollect) {
            if (itemCounts.ContainsKey(s)) {
                itemCounts[s]++;
            } else {
                itemCounts.Add(s, 1);
            }
        }

        foreach (FlowerColor s in collectedFlowers) {
            if (itemCounts.ContainsKey(s)) {
                itemCounts[s]--;
            }
            else {
                return false;
            }
        }
        return itemCounts.Values.All(c => c == 0);
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Load data");
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Save data");
    }
}
