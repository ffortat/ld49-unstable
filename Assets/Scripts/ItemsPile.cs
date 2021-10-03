using UnityEngine;

[RequireComponent(typeof(WindController))]
[RequireComponent(typeof(PileReactionController))]
public class ItemsPile : MonoBehaviour
{
    [SerializeField]
    private float blowPercentageOnPile = 3f;
    [SerializeField]
    private float runPercentageOnPile = 0.1f;

    [SerializeField]
    private float pileFallExponential = 1f;
    [SerializeField]
    private float pileFallDividingFactor = 360f;

    [SerializeField]
    private float dropAngle = 45f;

    private Vector3 pileNormal = Vector3.up;

    private PileReactionController pileReactionController = null;
    private WindController windController = null;

    private void Awake()
    {
        pileReactionController = GetComponent<PileReactionController>();
        windController = GetComponent<WindController>();
    }

    private void FixedUpdate()
    {
        pileNormal = (pileNormal + windController.WindBlow * blowPercentageOnPile / 100f).normalized;
        Vector3 velocity = pileReactionController.MoveCharacter(pileNormal);
        float pileAngle = Vector3.Angle(Vector3.up, pileNormal);
        float fallFactor = Mathf.Pow(pileAngle, pileFallExponential) / Mathf.Pow(pileFallDividingFactor, pileFallExponential);
        //Debug.Log("Angle " + Vector3.Angle(Vector3.up, pileNormal));
        pileNormal = (pileNormal + new Vector3(pileNormal.x, 0, pileNormal.z) * fallFactor).normalized;
        pileNormal = (pileNormal - velocity * runPercentageOnPile / 100f).normalized;
        ForDebug(transform.position, pileNormal * 3, Color.red);

        if (Vector3.Angle(Vector3.up, pileNormal) > dropAngle)
        {
            pileReactionController.DropPile();
        }
    }
    public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Debug.DrawRay(pos, direction, color);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
        Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
    }
}
