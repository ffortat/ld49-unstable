using UnityEngine;

public class Level : MonoBehaviour
{
    private StartPoint start = null;
    private FinishLine finish = null;

    private void Awake()
    {
        start = GetComponentInChildren<StartPoint>();
        finish = GetComponentInChildren<FinishLine>();

        Debug.Assert(start, "Missing component StartPoint in level " + name);
        Debug.Assert(finish, "Missing component FinishLine in level " + name);
    }

    public StartPoint Start { get => start; }
    public FinishLine Finish { get => finish; }
}
