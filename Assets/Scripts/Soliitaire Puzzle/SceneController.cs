using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene()
                .buildIndex + 1, LoadSceneMode.Single);
    }

    public void LoadSavedLevel()
    {
        if (PlayerPrefs.GetInt("Player Level") > 1)
            SceneManager.LoadScene(PlayerPrefs.GetInt("Player Level"));
        else SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Dev Only.
    /// </summary>
    public void ResetGame()
    {
        PlayerPrefs.SetInt("Player Level", 1);
        LoadNextScene();
        LoadMainScene();
    }
}
