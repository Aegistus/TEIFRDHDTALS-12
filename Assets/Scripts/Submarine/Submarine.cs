using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    [SerializeField] float moveForce = 10f;

    Rigidbody playerRigidbody;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRigidbody = FindAnyObjectByType<PlayerController>().GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.I))
        {
            MoveForward();
        }
    }

    public void MoveForward()
    {
        //playerCharacterController.enabled = false;
        rb.Move(transform.position + transform.forward * moveForce * Time.fixedDeltaTime, transform.rotation);
        playerRigidbody.Move(playerRigidbody.position + transform.forward * moveForce * Time.fixedDeltaTime, playerRigidbody.rotation);
        //playerCharacterController.enabled = true;
    }
}
