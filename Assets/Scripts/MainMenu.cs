using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsPanel = null;
    [SerializeField]
    private GameObject selectLevelPanel = null;

    private SceneLoader sceneLoader = null;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        Debug.Assert(sceneLoader, "Missing SceneLoader in the scene from " + name);
        Debug.Assert(creditsPanel, "Missing CreditsPanel on " + name);
    }

    private void Start()
    {
        StartCoroutine(DelayAddListener());
    }

    private IEnumerator DelayAddListener()
    {
        yield return null;

        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.AddOnOpenCreditsListener(OpenCredits);
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

    public void SelectLevel()
    {
        selectLevelPanel.SetActive(true);
    }

    public void CloseLevelSelection()
    {
        selectLevelPanel.SetActive(false);
    }

    public void StartLevel(int index)
    {
        sceneLoader.LoadLevel(index);
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

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
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
