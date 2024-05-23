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
    private InputManager inputManager;
    private AudioManager audioManager;

    [SerializeField] public GameObject WinMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject lostMenu;
    [SerializeField] private GameObject ingameMenu;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        inputManager = InputManager.Instance;
        audioManager = AudioManager.Instance;

        gameManager.OnFlowerEntered += HandleFlowerEntered;
        gameManager.OnBeehiveMissed += HandleOnBeehiveMissed;
        gameManager.OnObstacleEntered += HandleObstacleEntered;
        gameManager.OnPauseButtonClicked += HandlePauseButtonClicked;
        gameManager.OnResumeButtonClicked += HandleResumeButtonClicked;
        gameManager.OnRestartButtonClicked += HandleRestartButtonClicked;

        inputManager.OnEscapePressed  += HandlePauseButtonClicked;
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
        audioManager.StopMusic();
        PlaySFX(0);
    }
    private void HandleObstacleEntered()
    {
        lostMenu.SetActive(true);
        audioManager.StopMusic();
        PlaySFX(0);
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
        audioManager.PlayMusic();
    }

    private void PlaySFX(int type)
    {
        if (audioManager != null)
        {
            switch (type)
            {
                case 0:
                    audioManager.PlaySFX(audioManager.gameOverClip);
                    break;
            }
        }
        else
        {
            Debug.LogError("AudioManager instance not found");
        }
    }
}
