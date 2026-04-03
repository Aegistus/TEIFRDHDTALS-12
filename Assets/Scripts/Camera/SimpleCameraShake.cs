using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraShake : MonoBehaviour
{
    public float duration;
    public float magnitude;

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    public IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
