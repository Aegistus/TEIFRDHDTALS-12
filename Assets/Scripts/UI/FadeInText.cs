using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInText : MonoBehaviour
{
    [SerializeField] float fadeTime = 3f;

    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.alpha = 0;
    }

    public void Fade()
    {
        if (text != null)
        {
            StartCoroutine(FadeCoroutine());
        }
    }

    IEnumerator FadeCoroutine()
    {
        while (text.alpha < 1)
        {
            text.alpha += Time.deltaTime * fadeTime;
            yield return null;
        }
    }
}
