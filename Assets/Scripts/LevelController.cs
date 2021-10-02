using UnityEngine;

[RequireComponent(typeof(Timer))]
public class LevelController : MonoBehaviour
{
    [SerializeField]
    private PileReactionController characterPrefab = null;
    [SerializeField]
    private Level[] levels = new Level[0];

    private int levelIndex = 0;

    private PileReactionController currentCharacter = null;
    private Timer timer = null;

    private void Awake()
    {
        timer = GetComponent<Timer>();
    }

    private void Start()
    {
        LoadLevel(levelIndex);
    }

    private void LoadLevel(int index)
    {
        if (index >= 0 && index < levels.Length)
        {
            Level currentLevel = Instantiate(levels[index]);
            
            currentCharacter = Instantiate(characterPrefab);
            currentCharacter.AddOnFirstMoveListener(StartLevel);
            currentCharacter.AddOnStopListener(GameOver);
            currentCharacter.AddOnFinishLevelListener(FinishLevel);

            currentCharacter.transform.position = currentLevel.Start.transform.position;
        }
    }

    private void StartLevel()
    {
        timer.StartTimer();
    }

    private void GameOver()
    {
        timer.StopTimer();
        // TODO trigger gameover
    }

    private void FinishLevel()
    {
        timer.StopTimer();
        // TODO trigger win, save score, next level
    }
}
