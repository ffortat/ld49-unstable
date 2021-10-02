using UnityEngine;

[RequireComponent(typeof(WindController))]
[RequireComponent(typeof(CMF.Mover))]
public class CharacterController : CMF.Controller
{
    [SerializeField]
    private float velocityFactor = 5f;

    private CMF.Mover mover = null;
    private WindController windController = null;

    private void Awake()
    {
        mover = GetComponent<CMF.Mover>();
        windController = GetComponent<WindController>();
    }

    private void FixedUpdate()
    {
        mover.SetVelocity(GetVelocity());
    }

    public override Vector3 GetMovementVelocity()
    {
        return Vector3.zero;
    }

    public override Vector3 GetVelocity()
    {
        return velocityFactor * windController.WindBlow;
    }

    public override bool IsGrounded()
    {
        return true;
    }
}
