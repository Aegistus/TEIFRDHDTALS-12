using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidMarks : MonoBehaviour
{
    [SerializeField] WheelCollider wheel;
    [SerializeField] TrailRenderer trail;

    private void Update()
    {
        wheel.GetGroundHit(out WheelHit hit);
        if (hit.collider == null)
        {
            trail.emitting = false;
        }
        else
        {
            trail.emitting = true;
        }
    }
}
