using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FinishLine : MonoBehaviour
{
    private BoxCollider boxCollider = null;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PileReactionController controller = other.GetComponent<PileReactionController>();

        if (controller)
        {
            controller.ReachFinishLine();
        }
    }
}
