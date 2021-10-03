using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsPanel = null;

    private SceneLoader sceneLoader = null;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        Debug.Assert(sceneLoader, "Missing SceneLoader in the scene from " + name);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (creditsPanel.activeInHierarchy)
            {
                CloseCredits();
            }
            else
            {
                Exit();
            }
        }
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
        sceneLoader.LoadCredits(creditsPanel);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Application.OpenURL("https://ldjam.com/events/ludum-dare/49/where-the-wind-blows");
#else
        Application.Quit();
#endif
    }
}
