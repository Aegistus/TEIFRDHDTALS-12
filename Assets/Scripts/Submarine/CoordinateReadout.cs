using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoordinateReadout : MonoBehaviour
{
    enum Coordinate { X, Y, Degrees}

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
        else if (coordinate == Coordinate.Y)
        {
            text.text = ((int)sub.transform.position.z).ToString();
        }
        else if (coordinate == Coordinate.Degrees)
        {
            text.text = ((int)sub.transform.eulerAngles.y).ToString() + " Deg";
        }
    }
}
