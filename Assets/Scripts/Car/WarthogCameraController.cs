using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarthogCameraController : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float smoothSpeed = 5f;
    [SerializeField] float mouseSensitivity = 200f;
    [SerializeField] float cameraVerticalMinAngle = -80f;
    [SerializeField] float cameraVerticalMaxAngle = 80f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float verticalMovement = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float horizontalMovement = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float xRotation = transform.eulerAngles.x + verticalMovement;
        float yRotation = transform.eulerAngles.y + horizontalMovement;

        if (xRotation > 180)
        {
            xRotation -= 360;
        }
        xRotation = Mathf.Clamp(xRotation, cameraVerticalMinAngle, cameraVerticalMaxAngle);

        transform.eulerAngles = new Vector3(xRotation, yRotation, transform.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, smoothSpeed * Time.deltaTime);
    }
}
