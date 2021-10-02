using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<SceneLoader>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void LoadLeaderboard()
    {

    }

    public void LoadCredits()
    {

    }

    public void LoadOptions()
    {

    }
}
