using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class SceneManagerScript : Singleton<SceneManagerScript>
{
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
