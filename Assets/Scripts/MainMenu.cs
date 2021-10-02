using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private SceneLoader sceneLoader = null;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        Debug.Assert(sceneLoader, "Missing SceneLoader in the scene from " + name);
    }

    public void Play()
    {
        sceneLoader.LoadLevel();
    }

    public void Leaderboard()
    {
        sceneLoader.LoadLeaderboard();
    }

    public void Options()
    {
        sceneLoader.LoadOptions();
    }

    public void Credits()
    {
        sceneLoader.LoadCredits();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
