using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelBoard : MonoBehaviour, IDataPersistance
{
    private int levelsCleared;
    private List<Button> levelButtons;

    private void OnEnable()
    {
        levelButtons = GetComponentsInChildren<Button>().ToList();
    }

    public void LoadData(GameData data)
    {
        levelsCleared = data.levelsCleared;
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
                // EACH LEVEL BUTTON NEEDS TO HAVE A CORRESPONDING TAG THAT CAN BE PARSED TO INT
                int levelIndex = int.Parse(button.tag);
                button.interactable = levelIndex <= unlockedLevels;
            }
            catch (FormatException)
            {
                Debug.Log("Unparsable level button tag - disabling level anyway");
                button.interactable = false;
            }
        });
    }
}
