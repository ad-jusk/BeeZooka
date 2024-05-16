using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : Singleton<GameManager>
{
    #region GameEvents

    public delegate void BeehiveEntered();
    public event BeehiveEntered OnBeehiveEntered;
    public delegate void BeehiveMissed();
    public event BeehiveMissed OnBeehiveMissed;

    public delegate void FlowerEntered(FlowerColor flowerColor);
    public event FlowerEntered OnFlowerEntered;

    #endregion


    #region  GameStats

    public List<FlowerColor> flowersToCollect;
    public List<FlowerColor> collectedFlowers;

    #endregion

    private void Awake() {
        flowersToCollect = new() { FlowerColor.RED, FlowerColor.PURPLE };
        collectedFlowers = new();
    }

    public void NotifyBeehiveEntered() {
        OnBeehiveEntered?.Invoke();
    }

    public void NotifyBeehiveMissed() {
        OnBeehiveMissed?.Invoke();
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
}
