using System.Collections;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class UiManager : Singleton<UiManager>
{
    private GameManager gameManager;
    private InputManager inputManager;
    private AudioManager audioManager;

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

        inputManager.OnEscapePressed += HandlePauseButtonClicked;
    }

    private void OnEnable()
    {
        if(LevelManager.ShowToDoCanvas)
        {
            toDoCanvas.SetActive(true);
            LevelManager.ShowToDoCanvas = false;
            StartCoroutine(HideToDoCanvas());
        }
    }

    private IEnumerator HideToDoCanvas()
    {
        yield return new WaitForSeconds(2);
        toDoCanvas.SetActive(false);
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

    private void PlaySFX(int type)
    {
        if (audioManager != null)
        {
            switch (type)
            {
                case 0:
                    audioManager.PlaySFX(AudioClipType.GameOver);
                    break;
            }
        }
        else
        {
            Debug.LogError("AudioManager instance not found");
        }
    }
    private IEnumerator WaitForAnimationToFinish()
    {
        yield return new WaitForSeconds(2.0f);
        lostMenu.SetActive(true);
    }
}
