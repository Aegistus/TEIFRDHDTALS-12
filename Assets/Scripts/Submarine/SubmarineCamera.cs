using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineCamera : MonoBehaviour
{
    [SerializeField] float photoFadeDelay = 5f;
    [SerializeField] float coverFadeSpeed = 1f;
    [SerializeField] float photoFadeSpeed = 1f;
    [SerializeField] Camera subCamera;
    [SerializeField] RawImage cover;
    [SerializeField] RawImage actualImage;

    bool canTakePhoto = true;
    float photoTakeDelay = 3f;

    private void Start()
    {
        actualImage.color = actualImage.color.SetAlpha(0);
    }

    public void TakePhoto()
    {
        if (canTakePhoto)
        {
            subCamera.Render();
            cover.color = cover.color.SetAlpha(1);
            actualImage.color = actualImage.color.SetAlpha(1);
            canTakePhoto = false;
            StopAllCoroutines();
            StartCoroutine(FadeCover());
            StartCoroutine(FadePhoto());
            StartCoroutine(PhotoCooldown());
        }
    }

    IEnumerator FadeCover()
    {
        while (cover.color.a > 0)
        {
            cover.color = cover.color.SetAlpha(cover.color.a - (Time.deltaTime) * coverFadeSpeed);
            yield return null;
        }
    }

    IEnumerator FadePhoto()
    {
        yield return new WaitForSeconds(photoFadeDelay);
        while (actualImage.color.a > 0)
        {
            actualImage.color = actualImage.color.SetAlpha(actualImage.color.a - (Time.deltaTime) * photoFadeSpeed);
            yield return null;
        }
    }

    IEnumerator PhotoCooldown()
    {
        yield return new WaitForSeconds(photoTakeDelay);
        canTakePhoto = true;
    }

}
