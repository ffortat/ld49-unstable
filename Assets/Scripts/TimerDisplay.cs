using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI bestTime = null;
    [SerializeField]
    private TextMeshProUGUI personnalBestTime = null;
    [SerializeField]
    private TextMeshProUGUI currentTime = null;

    [SerializeField]
    private Timer timer = null;

    private void Awake()
    {
        Debug.Assert(bestTime, "Missing BestTime on " + name);
        Debug.Assert(personnalBestTime, "Missing PersonnalBestTime on " + name);
        Debug.Assert(currentTime, "Missing CurrentTime on " + name);
    }

    private void Update()
    {
        currentTime.text = timer.DisplayTime;
    }
}
