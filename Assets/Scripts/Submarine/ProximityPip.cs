using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityPip : MonoBehaviour
{
    [SerializeField] ProximityDetector.Direction direction;
    [SerializeField] Material activeMat;
    [SerializeField] Material inactiveMat;

    float maxInterval = 2f;
    float minInterval = .5f;

    AudioSource sound;
    MeshRenderer rend;
    ProximityDetector detector;
    bool active = false;
    float distance = float.MaxValue;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        rend = GetComponent<MeshRenderer>();
        detector = FindAnyObjectByType<ProximityDetector>();
        StartCoroutine(Coroutine());
    }

    private void Update()
    {
        Check();
    }

    private void Check()
    {
        active = false;
        switch (direction)
        {
            case ProximityDetector.Direction.Front: active = detector.FrontDetected;
                distance = detector.FrontDistance;
                break;
            case ProximityDetector.Direction.Back: active = detector.BackDetected;
                distance = detector.BackDistance;
                break;
            case ProximityDetector.Direction.Left: active = detector.LeftDetected;
                distance = detector.LeftDistance;
                break;
            case ProximityDetector.Direction.Right: active = detector.RightDetected;
                distance = detector.RightDistance;
                break;
        }
        if (active)
        {
            rend.material = activeMat;
        }
        else
        {
            rend.material = inactiveMat;
        }
    }

    IEnumerator Coroutine()
    {
        while (true)
        {
            if (active)
            {
                SoundManager.Instance.PlaySoundAtPosition("Proximity_Detector", transform.position);
                float proportionalDistance = distance / detector.MaxDistance;
                float delay = (maxInterval - minInterval) * proportionalDistance + minInterval;
                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return null;
            }
        }
    }

}
