using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelBoard : MonoBehaviour, IDataPersistance
{
    private int levelsCleared;
    private SerializableDictionary<int, int> levelIndexToCollectables;
    private List<Button> levelButtons;

    private void OnEnable()
    {
        levelButtons = GetComponentsInChildren<Button>().ToList();
    }

    public void LoadData(GameData data)
    {
        levelsCleared = data.levelsCleared;
        levelIndexToCollectables = data.levelIndexToCollectables;
        DisableLockedLevels();
    }

    public void SaveData(ref GameData data)
    {
        // NOTHING
    }

    private void DisableLockedLevels()
    {
        // +1 IS THE NEXT LEVEL
        int unlockedLevels = levelsCleared + 1;

        levelButtons.ForEach(button =>
        {
            try
            {
                GameObject collectibles = button.transform.Find("Collectibles").gameObject;

                // EACH LEVEL BUTTON NEEDS TO HAVE A CORRESPONDING TAG THAT CAN BE PARSED TO INT
                int levelIndex = int.Parse(button.tag);
                int score = 0;
                if (levelIndexToCollectables != null && levelIndexToCollectables.ContainsKey(levelIndex))
                {
                    score = levelIndexToCollectables[levelIndex];
                }
                button.interactable = levelIndex <= unlockedLevels;
                if (collectibles != null && score != 0)
                {
                    for (int i = 1; i <= score; i++)
                    {
                        collectibles.transform.Find("Collected" + i).gameObject.SetActive(true);
                    }
                }
            }
            catch (FormatException)
            {
                Debug.Log("Unparsable level button tag - disabling level anyway");
                button.interactable = false;
            }
        });
    }
}
