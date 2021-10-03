using UnityEngine;

[RequireComponent(typeof(Timer))]
public class LevelController : MonoBehaviour
{
    [SerializeField]
    private PileReactionController characterPrefab = null;
    [SerializeField]
    private Level[] levels = new Level[0];

    [SerializeField]
    private RectTransform pauseMenu = null;
    [SerializeField]
    private RectTransform gameOver = null;
    [SerializeField]
    private RectTransform saveScore = null;

    private bool isPaused = false;
    private int levelIndex = 0;

    private Level currentLevel = null;
    private PileReactionController currentCharacter = null;

    private Timer timer = null;
    private SceneLoader sceneLoader = null;

    private void Awake()
    {
        timer = GetComponent<Timer>();
        sceneLoader = FindObjectOfType<SceneLoader>();

        Debug.Assert(sceneLoader, "Missing SceneLoader in this scene from " + name);
    }

    private void Start()
    {
        LoadLevel(levelIndex);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
            {
                ResumeLevel();
            }
            else
            {
                PauseLevel();
            }
        }
    }

    public void PauseLevel()
    {
        timer.Pause();
        isPaused = true;
        pauseMenu.gameObject.SetActive(true);
        LockLevel();
    }

    public void ResumeLevel()
    {
        pauseMenu.gameObject.SetActive(false);
        UnlockLevel();
        isPaused = false;
        timer.Resume();
    }

    public void RestartLevel()
    {
        DestroyLevel();
        LoadLevel(levelIndex);
        gameOver.gameObject.SetActive(false);
        UnlockLevel();
    }

    public void NextLevel()
    {
        levelIndex += 1;

        if (levelIndex < levels.Length)
        {
            DestroyLevel();
            LoadLevel(levelIndex);
            saveScore.gameObject.SetActive(false);
        }
        else
        {
            QuitLevel();
        }
    }

    public void QuitLevel()
    {
        sceneLoader.LoadCredits();
    }

    private void LoadLevel(int index)
    {
        if (index >= 0 && index < levels.Length)
        {
            currentLevel = Instantiate(levels[index]);

            currentCharacter = Instantiate(characterPrefab);
            currentCharacter.AddOnFirstMoveListener(StartLevel);
            currentCharacter.AddOnStopListener(GameOver);
            currentCharacter.AddOnFinishLevelListener(FinishLevel);

            currentCharacter.transform.position = currentLevel.Start.transform.position;

            timer.ResetTimer();
        }
    }

    private void StartLevel()
    {
        timer.StartTimer();
    }

    private void GameOver()
    {
        timer.StopTimer();
        gameOver.gameObject.SetActive(true);
        LockLevel();
    }

    private void LockLevel()
    {
        if (currentCharacter)
        {
            currentCharacter.Lock();
        }
    }

    private void UnlockLevel()
    {
        currentCharacter.Unlock();
    }

    private void DestroyLevel()
    {
        if (currentCharacter)
        {
            Destroy(currentCharacter.gameObject);
            currentCharacter = null;
        }

        if (currentLevel)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        }
    }

    private void FinishLevel()
    {
        timer.StopTimer();
        LockLevel();
        saveScore.gameObject.SetActive(true);
    }
}
