using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform head;
	public Transform targetTransform;
    public Transform cameraTransform;
    public Transform holdTarget;
    public Camera frontCamera;
    public Camera backCamera;
	public float smoothSpeed = .5f;
    public float mouseSensitivity = 500f;
    public float cameraVerticalMaxAngle = 80f;
    public float cameraVerticalMinAngle = -80f;
    public Vector3 normalOffset;
    public Vector3 aimedOffset;
    public float aimSpeed = 10f;

    float xRotation;
    CameraShake camShake;
    AgentEquipment equipment;
    PlayerController playerController;

    void Start()
    {
        equipment = targetTransform.GetComponent<AgentEquipment>();
        equipment.OnWeaponChange += Equipment_OnWeaponChange; ;
        camShake = GetComponentInChildren<CameraShake>();
        playerController = FindObjectOfType<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Equipment_OnWeaponChange()
    {
        equipment.CurrentWeaponAttack.OnRecoil.AddListener(ScreenShake);
    }

    void Update()
	{
        var mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 85f);

        transform.position = head.position + head.localToWorldMatrix.MultiplyVector(normalOffset);
        transform.rotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y + mouseX, transform.rotation.eulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

        if (playerController.Aim)
        {
            holdTarget.localPosition = Vector3.Lerp(holdTarget.localPosition, aimedOffset, aimSpeed * Time.deltaTime);
        }
        else
        {
            holdTarget.localPosition = Vector3.Lerp(holdTarget.localPosition, normalOffset, aimSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            backCamera.gameObject.SetActive(true);
            frontCamera.gameObject.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            backCamera.gameObject.SetActive(false);
            frontCamera.gameObject.SetActive(true);
        }
    }



    void ScreenShake()
    {
        camShake.StartShake(equipment.CurrentWeaponAttack.camShakeProperties);
    }
}
