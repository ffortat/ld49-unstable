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

    [SerializeField]
    private AudioSource walkSound = null;

    private bool hasMoved = false;
    private bool isLocked = false;

    private float sinceLastSound = 0f;
    private float nextSound = 0f;

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

    private void FixedUpdate()
    {
        mover.CheckForGround();

        if (!mover.IsGrounded())
        {
            Stop();
        }
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
#if UNITY_EDITOR
            ItemsPile.ForDebug(transform.position, velocity, Color.green);
#endif

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
                Stop();
            }
        }

        mover.SetVelocity(GetVelocity());
        PlayWalkSound();
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

    public void DropPile()
    {
        Stop();
        // todo animation ? some delay before stop
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
        return mover.IsGrounded();
    }

    private void PlayWalkSound()
    {
        if (walkSound)
        {
            nextSound = velocity.magnitude == 0 ? float.MaxValue : 0.5f / velocity.magnitude;
            sinceLastSound += Time.deltaTime;

            if (sinceLastSound >= nextSound)
            {
                walkSound.Play();
                sinceLastSound = 0f;
            }
        }
    }

    private void Stop()
    {
        onStop?.Invoke();
    }

    public bool IsLocked { get => isLocked; }
}
