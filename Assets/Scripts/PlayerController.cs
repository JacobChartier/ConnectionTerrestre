using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
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
}
