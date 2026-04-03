using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 1f;

    Rigidbody playerRigidbody;
    Rigidbody rb;
    Transform cameraHolder;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRigidbody = FindAnyObjectByType<PlayerController>().GetComponent<Rigidbody>();
        cameraHolder = FindAnyObjectByType<CameraController>().transform;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.I))
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.K))
        {
            MoveBackward();
        }
        if (Input.GetKey(KeyCode.J))
        {
            RotateLeft();
        }
        if (Input.GetKey(KeyCode.L))
        {
            RotateRight();
        }
    }

    public void MoveForward()
    {
        var moveVector = moveSpeed * Time.fixedDeltaTime * transform.forward;
        rb.Move(transform.position + moveVector, transform.rotation);
        playerRigidbody.MovePosition(playerRigidbody.position + moveVector);
    }

    public void MoveBackward()
    {
        var moveVector = moveSpeed * Time.fixedDeltaTime * -transform.forward;
        rb.Move(transform.position + moveVector, transform.rotation);
        playerRigidbody.MovePosition(playerRigidbody.position + moveVector);
    }

    public void RotateLeft()
    {
        rb.transform.Rotate(Vector3.up, -rotateSpeed * Time.fixedDeltaTime);
    }

    public void RotateRight()
    {
        rb.transform.Rotate(Vector3.up, rotateSpeed * Time.fixedDeltaTime);
    }
}
