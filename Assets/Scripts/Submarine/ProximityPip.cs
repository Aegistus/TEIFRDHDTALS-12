using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class ProximityPip : MonoBehaviour
{
    [SerializeField] ProximityDetectors.Direction direction;
    [SerializeField] Material activeMat;
    [SerializeField] Material inactiveMat;

    float maxInterval = 2f;
    float minInterval = .5f;
    float maxDistance = 10f;

    AudioSource sound;
    MeshRenderer rend;
    ProximityDetectors detector;
    bool active = false;
    float distance = float.MaxValue;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        rend = GetComponent<MeshRenderer>();
        detector = FindAnyObjectByType<ProximityDetectors>();
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
            case ProximityDetectors.Direction.Front: active = detector.FrontDetected;
                distance = detector.FrontDistance;
                break;
            case ProximityDetectors.Direction.Back: active = detector.BackDetected;
                distance = detector.BackDistance;
                break;
            case ProximityDetectors.Direction.Left: active = detector.LeftDetected;
                distance = detector.LeftDistance;
                break;
            case ProximityDetectors.Direction.Right: active = detector.RightDetected;
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
                float proportionalDistance = distance / maxDistance;
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
