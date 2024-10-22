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
    public delegate void SelectLevelButtonClicked();
    public event SelectLevelButtonClicked OnSelectLevelButtonClicked;
    public delegate void FlowerEntered(FlowerColor flowerColor);
    public event FlowerEntered OnFlowerEntered;
    public delegate void FlowersCollected();
    public event FlowersCollected OnFlowersCollected;
    #endregion


    #region  GameStats

    public List<FlowerColor> flowersToCollect;
    public List<FlowerColor> collectedFlowers;
    public int collectedCollectibles;

    #endregion
    private AudioManager audioManager;

    [SerializeField]
    private bool hasPlayedSound = false;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
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
                flowersToCollect = new() { FlowerColor.RED, FlowerColor.RED, FlowerColor.RED, FlowerColor.RED };
                break;
            case 5:
                flowersToCollect = new() { FlowerColor.BLUE, FlowerColor.PINK };
                break;
            case 6:
                flowersToCollect = new() { FlowerColor.RED, FlowerColor.RED, FlowerColor.BLUE };
                break;
            case 7:
                flowersToCollect = new() { FlowerColor.PINK };
                break;
            case 8:
                flowersToCollect = new() { FlowerColor.PINK, FlowerColor.PINK, FlowerColor.RED, FlowerColor.RED, FlowerColor.BLUE, FlowerColor.BLUE };
                break;
            case 9:
                flowersToCollect = new() { FlowerColor.RED };
                break;
            case 10:
                flowersToCollect = new() { FlowerColor.RED, FlowerColor.BLUE };
                break;
            default:
                Debug.Log("Scene is not a level");
                break;
        }
        collectedFlowers = new();
        collectedCollectibles = 0;
    }

    public void NotifyBeehiveEntered()
    {
        OnBeehiveEntered?.Invoke();
    }

    public void NotifyObstacleEntered()
    {
        OnObstacleEntered?.Invoke();
        if (!hasPlayedSound)
        {
            hasPlayedSound = true;
            audioManager.PlaySFX(AudioClipType.ObstacleHit);
        }
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

    public void NotifyCollectibleCollected()
    {
        collectedCollectibles++;
    }

    public void NotifySelectLevelButtonClicked()
    {
        OnSelectLevelButtonClicked?.Invoke();
    }

    public void NotifyAllFlowersCollected()
    {
        OnFlowersCollected?.Invoke();
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
        bool allCollected = itemCounts.Values.All(c => c == 0);
        if (allCollected)
        {
            NotifyAllFlowersCollected();
        }
        return allCollected;
    }
}
