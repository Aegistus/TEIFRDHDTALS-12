using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestChecker : MonoBehaviour
{
    [SerializeField] Transform[] pointsOfInterest;
    [SerializeField] float positionErrorAllowance = 1f;
    [SerializeField] float rotationErrorAllowance = 1f;
}
