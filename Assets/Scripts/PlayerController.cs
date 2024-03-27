using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private const float LIM_XZ = 450;

    [SerializeField] private float speed = 1;
    Rigidbody rb;

    [SerializeField] private float verticalInput;
    [SerializeField] private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        MovePlayerRelativeToCamera();
        FailSafe();
    }

    private void Move()
    {
        Vector2 move = InputManager.mouvementInput;
        rb.velocity = new Vector3(((move.x * speed) * Time.deltaTime) / 20, rb.velocity.y, ((move.y * speed) * Time.deltaTime) / 20);
    }

    private void MovePlayerRelativeToCamera()
    {
        verticalInput = InputManager.mouvementInput.y;
        horizontalInput = InputManager.mouvementInput.x;

        Vector3 forward = transform.InverseTransformVector(Camera.main.transform.forward).normalized;
        Vector3 right = transform.InverseTransformVector(Camera.main.transform.right).normalized;

        forward.y = 0;
        right.y = 0;

        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeToVerticalInput = (verticalInput * forward);
        Vector3 rightRelativeToHorizontalInput = (horizontalInput * right);

        Vector3 cameraRelativeMovement = forwardRelativeToVerticalInput + rightRelativeToHorizontalInput;
        this.transform.Translate((cameraRelativeMovement * speed) * Time.deltaTime, Space.World);
    }

    private void FailSafe()
    {
        if (transform.position.y < -10)
        {
            //RaycastHit hit; //TODO: ça
            //if (Physics.Raycast(transform.position, Vector3.down, out hit))
            //{
            //    transform.position = hit.point + Vector3.up * 3;
            //}
            transform.position += Vector3.up * 100;
        }

        if (transform.position.x > LIM_XZ)
        {
            transform.position = new Vector3(LIM_XZ, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -LIM_XZ)
        {
            transform.position = new Vector3(-LIM_XZ, transform.position.y, transform.position.z);
        }

        if (transform.position.z > LIM_XZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, LIM_XZ);
        }
        else if (transform.position.z < -LIM_XZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -LIM_XZ);
        }
    }
}
