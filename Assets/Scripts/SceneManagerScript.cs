using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public static string GetCurrentSceneName() {
        return SceneManager.GetActiveScene().name;
    }
}
