using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 8f;
    public float jumpHeight = 2f; // Adjust this value for jump height

    public Transform groundCheck;
    public float groundDistance = 0.4f; // Sphere radius for ground check
    public LayerMask groundMask; // LayerMask to determine what constitutes ground

    private Vector3 moveDirection;
    private bool isGrounded;
    private bool jumpRequest;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Ensure that we don't rotate the player based on the physics simulation
        rb.freezeRotation = true;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDirection = (transform.right * x + transform.forward * z).normalized;

        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();

        if (jumpRequest)
        {
            // Assuming you want to jump up to a certain height, calculate the required initial velocity
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            jumpRequest = false;
        }
    }

    void MovePlayer()
    {
        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }
    }
}
