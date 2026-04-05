using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    [SerializeField] Transform[] leftSensors;
    [SerializeField] Transform[] rightSensors;
    [SerializeField] Transform[] frontSensors;
    [SerializeField] Transform[] backSensors;
    [SerializeField] float maxDistance = 20f;

    public float MaxDistance => maxDistance;
    public enum Direction { Front, Back, Left, Right }

    public bool FrontDetected { get; private set; } = false;
    public float FrontDistance { get; private set; } = float.MaxValue;
    public bool BackDetected {  get; private set; } = false;
    public float BackDistance { get; private set; } = float.MaxValue;
    public bool LeftDetected { get; private set; } = false;
    public float LeftDistance { get; private set; } = float.MaxValue;
    public bool RightDetected { get; private set; } = false;
    public float RightDistance { get; private set; } = float.MaxValue;

    Dictionary<Direction, Transform[]> sensors = new Dictionary<Direction, Transform[]>();

    private void Awake()
    {
        sensors = new Dictionary<Direction, Transform[]>()
        {
            { Direction.Front, frontSensors},
            { Direction.Back, backSensors},
            { Direction.Left, leftSensors},
            { Direction.Right, rightSensors},
        };
    }

    private void Update()
    {
        CheckDirection(Direction.Front);
        CheckDirection(Direction.Back);
        CheckDirection(Direction.Left);
        CheckDirection(Direction.Right);
    }

    public void CheckDirection(Direction dir)
    {
        float minDistance = float.MaxValue;
        for (int i = 0; i < sensors[dir].Length; i++)
        {
            if (Physics.Raycast(sensors[dir][i].position, sensors[dir][i].forward, out RaycastHit rayHit, maxDistance))
            {
                if (minDistance > rayHit.distance)
                {
                    minDistance = rayHit.distance;
                    switch (dir)
                    {
                        case Direction.Front: FrontDetected = true; break;
                        case Direction.Back: BackDetected = true; break;
                        case Direction.Left: LeftDetected = true; break;
                        case Direction.Right: RightDetected = true; break;
                    }
                }
            }
        }
        switch (dir)
        {
            case Direction.Front: FrontDistance = minDistance; break;
            case Direction.Back: BackDistance = minDistance; break;
            case Direction.Left: LeftDistance = minDistance; break;
            case Direction.Right: RightDistance = minDistance; break;
        }
        if (minDistance ==  float.MaxValue)
        {
            switch (dir)
            {
                case Direction.Front: FrontDetected = false; break;
                case Direction.Back: BackDetected = false; break;
                case Direction.Left: LeftDetected = false; break;
                case Direction.Right: RightDetected = false; break;
            }
        }
    }

}
