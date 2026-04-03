using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoordinateReadout : MonoBehaviour
{
    enum Coordinate { X, Y}

    [SerializeField] Coordinate coordinate;
    [SerializeField] TMP_Text text;

    Submarine sub;

    private void Start()
    {
        sub = FindAnyObjectByType<Submarine>();
    }

    private void Update()
    {
        if (coordinate == Coordinate.X)
        {
            text.text = ((int)sub.transform.position.x).ToString();
        }
        else
        {
            text.text = ((int)sub.transform.position.z).ToString();
        }
    }
}
