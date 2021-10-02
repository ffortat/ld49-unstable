using UnityEngine;

[RequireComponent(typeof(WindController))]
[RequireComponent(typeof(CMF.Mover))]
public class PileReactionController : CMF.Controller
{
    [SerializeField]
    private float velocityFactor = 75f;

    private Vector3 velocity = Vector3.zero;

    private CMF.Mover mover = null;
    private WindController windController = null;

    private void Awake()
    {
        mover = GetComponent<CMF.Mover>();
        windController = GetComponent<WindController>();
    }

    public Vector3 MoveCharacter(Vector3 pileNormal)
    {
        velocity = new Vector3(pileNormal.x, 0f, pileNormal.z) * velocityFactor * velocityFactor / 100f;
        ItemsPile.ForDebug(transform.position, velocity, Color.green);
        mover.SetVelocity(GetVelocity());
        return GetVelocity();
    }

    public override Vector3 GetMovementVelocity()
    {
        return Vector3.zero;
    }

    public override Vector3 GetVelocity()
    {
        return velocity;
    }

    public override bool IsGrounded()
    {
        return true;
    }
}
