using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 1f;

    public bool MoveForwards { get; set; } = false;
    public bool MoveBackwards { get; set; } = false;
    public bool LeftRotate { get; set; } = false;
    public bool RightRotate { get; set; } = false;

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
        if (MoveForwards || Input.GetKey(KeyCode.I))
        {
            MoveForward();
        }
        if (MoveBackwards || Input.GetKey(KeyCode.K))
        {
            MoveBackward();
        }
        if (LeftRotate || Input.GetKey(KeyCode.J))
        {
            RotateLeft();
        }
        if (RightRotate || Input.GetKey(KeyCode.L))
        {
            RotateRight();
        }
    }

    void MoveForward()
    {
        var moveVector = moveSpeed * Time.fixedDeltaTime * transform.forward;
        rb.Move(transform.position + moveVector, transform.rotation);
        playerRigidbody.MovePosition(playerRigidbody.position + moveVector);
    }

    void MoveBackward()
    {
        var moveVector = moveSpeed * Time.fixedDeltaTime * -transform.forward;
        rb.Move(transform.position + moveVector, transform.rotation);
        playerRigidbody.MovePosition(playerRigidbody.position + moveVector);
    }

    void RotateLeft()
    {
        rb.transform.Rotate(Vector3.up, -rotateSpeed * Time.fixedDeltaTime);
    }

    void RotateRight()
    {
        rb.transform.Rotate(Vector3.up, rotateSpeed * Time.fixedDeltaTime);
    }
}
