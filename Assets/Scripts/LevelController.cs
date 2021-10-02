using UnityEngine;

[RequireComponent(typeof(Timer))]
public class LevelController : MonoBehaviour
{
    [SerializeField]
    private PileReactionController characterPrefab = null;

    private PileReactionController currentCharacter = null;
    private Timer timer = null;

    private void Awake()
    {
        timer = GetComponent<Timer>();
    }

    private void Start()
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        currentCharacter = Instantiate(characterPrefab);
        currentCharacter.AddOnFirstMoveListener(StartLevel);
        currentCharacter.AddOnStopListener(GameOver);
    }

    private void StartLevel()
    {
        timer.StartTimer();
    }

    private void GameOver()
    {
        timer.StopTimer();
    }
}
