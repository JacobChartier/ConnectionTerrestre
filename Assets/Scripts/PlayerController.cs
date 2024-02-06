using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    Rigidbody rb;

    void Start()
    {
        //cache (caching)
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 move = InputManager.mouvementInput;
        rb.velocity = new Vector3(move.x * speed, rb.velocity.y, move.y * speed);
    }

}
