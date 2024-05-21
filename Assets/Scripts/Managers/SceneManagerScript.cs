using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class SceneManagerScript : Singleton<SceneManagerScript>
{
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    public void ReloadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
