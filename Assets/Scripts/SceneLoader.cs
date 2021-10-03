using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private UnityEvent onOpenCredits = new UnityEvent();

    private void Awake()
    {
        if (FindObjectsOfType<SceneLoader>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddOnOpenCreditsListener(UnityAction callback)
    {
        onOpenCredits.AddListener(callback);
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
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            SceneManager.LoadScene("MainMenu");
            StartCoroutine(DelayFunction(OpenCreditsPanel));
        }
        else
        {
            OpenCreditsPanel();
        }
    }

    public void LoadOptions()
    {

    }

    private IEnumerator DelayFunction(UnityAction callback)
    {
        yield return null;
        yield return null;

        callback();
    }

    private void OpenCreditsPanel()
    {
        onOpenCredits?.Invoke();
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed");
    }
}
