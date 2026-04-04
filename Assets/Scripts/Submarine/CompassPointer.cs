using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassPointer : MonoBehaviour
{
    Submarine sub;

    private void Start()
    {
        sub = FindAnyObjectByType<Submarine>();
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(-123, 0, sub.transform.eulerAngles.y);
    }
}
