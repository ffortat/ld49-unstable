using UnityEngine;

[RequireComponent(typeof(WindController))]
[RequireComponent(typeof(CharacterController))]
public class ItemsPile : MonoBehaviour
{
    [SerializeField]
    private float blowPercentageOnPile = 3f;
    [SerializeField]
    private float runPercentageOnPile = 0.1f;

    private Vector3 pileNormal = Vector3.up;

    private CharacterController characterController = null;
    private WindController windController = null;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        windController = GetComponent<WindController>();
    }

    private void FixedUpdate()
    {
        pileNormal = (pileNormal + windController.WindBlow * blowPercentageOnPile / 100f).normalized;
        Vector3 velocity = characterController.MoveCharacter(pileNormal);
        // TODO ajouter de la chute selon l'angle de la pile
        pileNormal = (pileNormal - velocity * runPercentageOnPile / 100f).normalized;
        ForDebug(transform.position, pileNormal * 3, Color.red);
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
