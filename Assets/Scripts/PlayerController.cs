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
    }

    private void Update()
    {
        FailSafe();
    }

    private void Move()
    {
        Vector2 move = InputManager.mouvementInput;
        //rb.velocity = new Vector3(((move.x * speed) * Time.deltaTime) / 20, rb.velocity.y, ((move.y * speed) * Time.deltaTime) / 20);
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

        transform.rotation = Quaternion.identity; // répare un bug qui desync la rotation avec la caméra
    }

    private void FailSafe()
    {
        Ray r = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        // si il y a du sol dessous le joueur
        if (Physics.Raycast(r, out hit))
        {
            // si le joueur est dessous 
            if (Physics.Raycast(r, out hit, 1))
            {
                transform.position = hit.point + Vector3.up * 1;
            }
        }
        else
        {
            // mettre le joueur au sol dessus lui
            if (Physics.Raycast(new Ray(transform.position + new Vector3(0, 100, 0), Vector3.down), out hit))
            {
                transform.position = hit.point + Vector3.up * 1;
            }
        }

        if (transform.position.y < -10)
        {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Ray r = new Ray(transform.position, Vector3.up);
        Gizmos.DrawRay(r);
    }
}
