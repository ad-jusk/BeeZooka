using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

[DefaultExecutionOrder(-1)]

public class UiManager : Singleton<UiManager>
{
    private GameManager gameManager;
    [SerializeField] public GameObject WinMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject lostMenu;
    [SerializeField] private GameObject ingameMenu;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.OnFlowerEntered += HandleFlowerEntered;
        gameManager.OnBeehiveMissed += HandleOnBeehiveMissed;
        gameManager.OnPauseButtonClicked += HandlePauseButtonClicked;
        gameManager.OnRestartButtonClicked += HandleResumeButtonClicked;
        gameManager.OnRestartButtonClicked += HandleRestartButtonClicked;
    }

    private void HandleFlowerEntered(FlowerColor flowerColor)
    {
        
        Debug.Log("A flower of color " + flowerColor + " entered!");

        Transform textTransform = ingameMenu.transform.Find(flowerColor.ToString());
        if (textTransform != null)
        {
            TextMeshProUGUI textMeshPro = textTransform.GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.text = "1/1";
            }
            else
            {
                Debug.LogError("TextMeshPro component not found on the child GameObject.");
            }
        }
        else
        {
            Debug.LogError($"Child GameObject with the name '{flowerColor.ToString()}' not found under the canvas.");
        }
    }
    private void HandleOnBeehiveMissed()
    {
        lostMenu.SetActive(true);
    }
    private void HandlePauseButtonClicked()
    {
        pauseMenu.SetActive(true);
    }
    private void HandleResumeButtonClicked()
    {
        pauseMenu.SetActive(false);
    }
    private void HandleRestartButtonClicked()
    {
        lostMenu.SetActive(false);
        pauseMenu.SetActive(false);
        WinMenu.SetActive(false);
    }

    private void ChangeText(string text, string type)
    {

    }
}
