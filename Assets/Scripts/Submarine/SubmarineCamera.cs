using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineCamera : MonoBehaviour
{
    [SerializeField] Camera subCamera;

    bool canTakePhoto = true;
    float photoTakeDelay = 1f;

    public void TakePhoto()
    {
        if (canTakePhoto)
        {
            subCamera.Render();
            canTakePhoto = false;
            StartCoroutine(PhotoCooldown());
        }
    }

    IEnumerator PhotoCooldown()
    {
        yield return new WaitForSeconds(photoTakeDelay);
        canTakePhoto = true;
    }
}
