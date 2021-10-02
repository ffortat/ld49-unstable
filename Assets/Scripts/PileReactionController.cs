using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WindController))]
[RequireComponent(typeof(CMF.Mover))]
public class PileReactionController : CMF.Controller
{
    [SerializeField]
    private float velocityFactor = 75f;
    [SerializeField]
    private float stopFactor = 0.1f;

    private bool hasMoved = false;
    private bool isLocked = false;

    private Vector3 velocity = Vector3.zero;
    private UnityEvent onFirstMove = new UnityEvent();
    private UnityEvent onStop = new UnityEvent();
    private UnityEvent onFinishLevel = new UnityEvent();

    private CMF.Mover mover = null;
    private WindController windController = null;

    private void Awake()
    {
        mover = GetComponent<CMF.Mover>();
        windController = GetComponent<WindController>();
    }

    public Vector3 MoveCharacter(Vector3 pileNormal)
    {
        if (isLocked)
        {
            velocity = Vector3.zero;
        }
        else
        {
            velocity = new Vector3(pileNormal.x, 0f, pileNormal.z) * velocityFactor * velocityFactor / 100f;
            ItemsPile.ForDebug(transform.position, velocity, Color.green);

            if (!hasMoved)
            {
                if (velocity != Vector3.zero)
                {
                    hasMoved = true;
                    onFirstMove?.Invoke();
                }
            }
            else if (Vector3.SqrMagnitude(velocity) < stopFactor)
            {
                onStop?.Invoke();
            }
        }

        mover.SetVelocity(GetVelocity());
        return GetVelocity();
    }

    public void Lock()
    {
        isLocked = true;
        windController.Lock();
    }

    public void Unlock()
    {
        isLocked = false;
        windController.Unlock();
    }

    public void ReachFinishLine()
    {
        onFinishLevel?.Invoke();
    }

    public void AddOnFirstMoveListener(UnityAction callback)
    {
        onFirstMove.AddListener(callback);
    }

    public void AddOnStopListener(UnityAction callback)
    {
        onStop.AddListener(callback);
    }

    public void AddOnFinishLevelListener(UnityAction callback)
    {
        onFinishLevel.AddListener(callback);
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
