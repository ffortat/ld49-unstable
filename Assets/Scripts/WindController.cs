using UnityEngine;

public class WindController : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera = null;

    [SerializeField]
    private GameObject windIndicator = null;
    [SerializeField]
    private ParticleSystem windBlowEffect = null;

    [SerializeField]
    private float indicatorDistance = 2f;

    [SerializeField]
    private string blowButtonName = "Blow";

    private bool useController = false;
    private bool isCursorSet = false;
    private bool isBlowing = false;
    private bool isLocked = false;
    private float windStrength = 0f;
    private Plane abstractFloor;
    private Vector3 windDirection = new Vector3(0, 0, 1f);

    private void Awake()
    {
        Debug.Assert(windIndicator, "WindIndicator is missing on " + name);
        abstractFloor = new Plane(transform.up, transform.position);
    }

    private void Update()
    {
        if (!isLocked)
        {
            if (useController)
            {
                // TODO controls for controller
            }
            else
            {
                // default to mouse controls
                Ray mouseRay = playerCamera.ScreenPointToRay(Input.mousePosition);
                abstractFloor.SetNormalAndPosition(transform.up, transform.position);

                isCursorSet = abstractFloor.Raycast(mouseRay, out float positionOnRay) || positionOnRay != 0;
                Vector3 cursorPosition = mouseRay.GetPoint(positionOnRay);

                if (isCursorSet)
                {
                    Vector3 windVector = transform.position - cursorPosition;

                    windDirection = windVector.normalized;
                    windStrength = Mathf.Min(1, indicatorDistance / windVector.magnitude);
                }
            }

            Vector3 indicatorPosition = -windDirection * indicatorDistance;
            indicatorPosition.y = windIndicator.transform.localPosition.y;
            float strengthScale = 0.5f * windStrength;

            windIndicator.transform.localPosition = indicatorPosition;
            windIndicator.transform.localScale = new Vector3(2, strengthScale, strengthScale);
            windIndicator.transform.LookAt(new Vector3(transform.position.x, windIndicator.transform.position.y, transform.position.z));

            if (Input.GetButtonDown(blowButtonName))
            {
                isBlowing = true;
                windBlowEffect.Play();
            }
            else if (Input.GetButtonUp(blowButtonName))
            {
                isBlowing = false;
                windBlowEffect.Stop();
            }
        }
    }

    public void Lock()
    {
        isLocked = true;
        if (isBlowing)
        {
            isBlowing = false;
            windBlowEffect.Pause();
        }
    }

    public void Unlock()
    {
        if (windBlowEffect.isPaused)
        {
            windBlowEffect.Play();
            windBlowEffect.Stop();
        }
        isLocked = false;
    }

    public bool IsBlowing { get => isBlowing; }
    public float WindStrength { get => isBlowing ? windStrength : 0f; }
    public Vector3 WindDirection { get => windDirection; }
    public Vector3 WindBlow { get => windDirection * Mathf.Pow(WindStrength, 2); }
}
