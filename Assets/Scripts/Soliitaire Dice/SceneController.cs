using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] int _levelNumber;
    [SerializeField] string _levelName;

    public void LoadLevel(int level)
    {
        _levelNumber = level;
        SceneManager.LoadScene(level);
    }

    public void LoadLevel(string level)
    {
        _levelName = level;
        SceneManager.LoadScene(level);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene()
                    .buildIndex + 1, LoadSceneMode.Single);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene()
                .buildIndex - 1, LoadSceneMode.Single);
    }

    public void LoadSavedLevel()
    {
        if (PlayerPrefs.GetInt("Player Level") > 1)
            SceneManager.LoadScene(PlayerPrefs.GetInt("Player Level"));
        else SceneManager.LoadScene(1);
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
