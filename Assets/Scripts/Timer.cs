using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool isRunning = false;
    private float time = 0f;

    private void FixedUpdate()
    {
        if (isRunning)
        {
            time += Time.fixedDeltaTime;
        }
    }

    public void StartTimer()
    {
        time = 0f;
        Resume();
    }

    public void Pause()
    {
        isRunning = false;
    }

    public void Resume()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        Pause();
    }

    public bool IsRunning { get => isRunning; }
    public float CurrentTime { get => time; }
    public string DisplayTime {
        get {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time - minutes * 60f);
            int milliseconds = Mathf.FloorToInt((time % 1) * 1000);

            return minutes.ToString("D2") + ":" + seconds.ToString("D2") + ":" + milliseconds.ToString("D3");
        }
    }
}
