using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class UiManager : Singleton<UiManager>
{
    private GameManager gameManager;
    private InputManager inputManager;
    private AudioManager audioManager;
    private int currentNumber = 0;
    private int maxNumber = 1;

    [SerializeField]
    public GameObject WinMenu;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject lostMenu;

    [SerializeField]
    private GameObject ingameMenu;

    [SerializeField]
    private GameObject toDoCanvas;

    [SerializeField]
    private GameObject tutorialCanvas;

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
        gameManager.OnHomeButtonClicked += HandleHomeButtonClicked;
        gameManager.OnSelectLevelButtonClicked += HandleSelectLevelButtonClicked;

        inputManager.OnEscapePressed += HandlePauseButtonClicked;
    }

    private void OnEnable()
    {
        if (LevelManager.ShowToDoCanvas)
        {
            toDoCanvas.SetActive(true);
            LevelManager.ShowToDoCanvas = false;
            StartCoroutine(HideToDoCanvas());
        }
        else
        {
            if(tutorialCanvas != null)
            {
                tutorialCanvas.SetActive(true);
            }
        }
    }

    public void LevelCleared(int score)
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        WinMenu.SetActive(true);
        WinMenuSound();
        StartCoroutine(CollectiblesCollected(score));
    }


    private void HandleFlowerEntered(FlowerColor flowerColor)
    {
        Debug.Log("A flower of color " + flowerColor + " entered!");

        Transform textTransform = ingameMenu.transform.Find(flowerColor.ToString());
        if (textTransform != null)
        {
            TextMeshProUGUI textMeshPro = textTransform.GetComponentInChildren<TextMeshProUGUI>();
            string[] parts = textMeshPro.text.ToString().Split('/');

            if (textMeshPro != null)
            {
                if (parts.Length >= 2)
                {
                    currentNumber = int.Parse(parts[0]);
                    currentNumber++;
                    maxNumber = int.Parse(parts[1]);

                    Debug.Log("Current Number: " + currentNumber);
                    Debug.Log("Max Number: " + maxNumber);
                }
                textMeshPro.text = $"{currentNumber}/{maxNumber}";
            }
            else
            {
                Debug.LogError("TextMeshPro component not found on the child GameObject.");
            }
        }
        else
        {
            Debug.LogWarning($"Child GameObject with the name '{flowerColor}' not found under the canvas.");
        }
    }

    private void HandleOnBeehiveMissed()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        lostMenu.SetActive(true);
        audioManager.StopMusic();
        PlaySFX(0);
    }

    private void HandleObstacleEntered()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        audioManager.StopMusic();
        PlaySFX(0);
        StartCoroutine(WaitForAnimationToFinish());
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

    private void HandleHomeButtonClicked()
    {
        audioManager.PlayMusic();
    }
    private void HandleSelectLevelButtonClicked()
    {
        audioManager.PlayMusic();
    }
    private void PlaySFX(int type)
    {
        if (audioManager != null)
        {
            switch (type)
            {
                case 0:
                    audioManager.PlaySFX(AudioClipType.GameOver);
                    break;
                case 1:
                    audioManager.PlaySFX(AudioClipType.GameWon);
                    break;
            }
        }
        else
        {
            Debug.LogError("AudioManager instance not found");
        }
    }

    private IEnumerator HideToDoCanvas()
    {
        new WaitForSeconds(0.3f);
        Image targetImage = toDoCanvas.GetComponentsInChildren<Image>().FirstOrDefault(image => image.CompareTag("ProgressBar"));
        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            targetImage.fillAmount = Mathf.Lerp(1f, 0f, elapsed / duration);
            yield return null;
        }

        targetImage.fillAmount = 0f;
        toDoCanvas.SetActive(false);
        if(tutorialCanvas != null) 
        {
            tutorialCanvas.SetActive(true);
        }
    }

    private IEnumerator WaitForAnimationToFinish()
    {
        yield return new WaitForSeconds(2.0f);
        lostMenu.SetActive(true);
    }

    private IEnumerator CollectiblesCollected(int score)
    {
        GameObject collectibles = WinMenu.transform.Find("Collectibles").gameObject;
        yield return new WaitForSeconds(0.5f);
        for (int i = 1; i <= score; i++)
        {
            yield return new WaitForSeconds(0.3f);
            collectibles.transform.Find("Collected" + i).gameObject.SetActive(true);
            collectibles.transform.Find("Collected" + i).GetComponent<Animation>().Play();
        }
    }
    private void WinMenuSound()
    {
        audioManager.ChangeMusicVolume(0.5f);
        PlaySFX(1);
    }
}
