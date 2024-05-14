using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class SceneManagerScript : Singleton<SceneManagerScript>
{
    private const string START_SCENE = "StartScene";
    private const string GAME_SCENE = "GameScene";

    private InputManager inputManager;

    private void Awake() {
        inputManager = InputManager.Instance;
        inputManager.OnEscapePressed += EscapePressed;
    }

    private void EscapePressed() {

        string currentScene = SceneManager.GetActiveScene().name;

        switch(currentScene) {
            case START_SCENE:
                Application.Quit();
                break;
            case GAME_SCENE:
                ChangeScene(START_SCENE);
                break;
            default:
                Debug.LogError("Unsupported scene");
                break;
        }
    }

    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
