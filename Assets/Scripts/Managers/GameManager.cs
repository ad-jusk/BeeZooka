using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : Singleton<GameManager>
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
    public delegate void NextLevelButtonClicked();
    public event NextLevelButtonClicked OnNextLevelButtonClicked;
    public delegate void HomeButtonClicked();
    public event HomeButtonClicked OnHomeButtonClicked;

    public delegate void FlowerEntered(FlowerColor flowerColor);
    public event FlowerEntered OnFlowerEntered;

    #endregion


    #region  GameStats

    public List<FlowerColor> flowersToCollect;
    public List<FlowerColor> collectedFlowers;

    #endregion

    private void Awake()
    {
        int levelIndex = LevelIndexFromSceneExtractor.GetLevelIndex(SceneManager.GetActiveScene().name);
        switch (levelIndex)
        {
            case 1:
                flowersToCollect = new() { FlowerColor.RED, FlowerColor.PINK };
                break;
            case 2:
                flowersToCollect = new() { FlowerColor.RED, FlowerColor.PINK, FlowerColor.BLUE };
                break;
            case 3:
                flowersToCollect = new() { FlowerColor.RED, FlowerColor.RED, FlowerColor.BLUE };
                break;
            case 4:
                flowersToCollect = new () {FlowerColor.RED, FlowerColor.RED, FlowerColor.RED, FlowerColor.RED};
                break;
            case 5:
                flowersToCollect = new () {FlowerColor.BLUE, FlowerColor.PINK};
                break;
            default:
                Debug.Log("Scene is not a level");
                break;
        }
        collectedFlowers = new();
    }

    public void NotifyBeehiveEntered()
    {
        OnBeehiveEntered?.Invoke();
    }

    public void NotifyBeehiveMissed()
    {
        OnBeehiveMissed?.Invoke();
    }

    public void NotifyObstacleEntered()
    {
        OnObstacleEntered?.Invoke();
    }

    public void NotifyPauseButtonClicked()
    {
        OnPauseButtonClicked?.Invoke();
    }

    public void NotifyResumeButtonClicked()
    {
        OnResumeButtonClicked?.Invoke();
    }

    public void NotifyRestartButtonClicked()
    {
        OnRestartButtonClicked?.Invoke();
    }

    public void NotifyNextLevelButtonClicked()
    {
        OnNextLevelButtonClicked?.Invoke();
    }

    public void NotifyHomeButtonClicked()
    {
        OnHomeButtonClicked?.Invoke();
    }

    public void NotifyFlowerEntered(FlowerColor flowerColor)
    {
        collectedFlowers.Add(flowerColor);
        OnFlowerEntered?.Invoke(flowerColor);
    }

    public bool AllFlowersCollected()
    {
        if (collectedFlowers.Count == 0)
        {
            return false;
        }

        var itemCounts = new Dictionary<FlowerColor, int>();

        foreach (FlowerColor s in flowersToCollect)
        {
            if (itemCounts.ContainsKey(s))
            {
                itemCounts[s]++;
            }
            else
            {
                itemCounts.Add(s, 1);
            }
        }

        foreach (FlowerColor s in collectedFlowers)
        {
            if (itemCounts.ContainsKey(s))
            {
                itemCounts[s]--;
            }
        }
        return itemCounts.Values.All(c => c == 0);
    }
}
