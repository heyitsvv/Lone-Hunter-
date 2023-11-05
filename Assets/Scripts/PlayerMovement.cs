using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 8f;
    public float gravity = -30f;

    public GameObject quest;
    public QuestGiver questGiver;
    private bool questActive = false;

    Rigidbody rb;
    Vector3 velocity;

    public Transform jumpChecker;
    public LayerMask groundMask;
    bool canJump;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        quest.SetActive(questActive);
    }

    // Update is called once per frame
    void Update()
    {   
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift) && canJump)
        {
            speed = 14f;
        }
        else
        {
            speed = 8f;
        }

        controller.Move(move * speed * Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        canJump = Physics.CheckSphere(jumpChecker.position, 0.6f, groundMask);
        if (velocity.y < 0 && canJump)
        {
            velocity.y = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            velocity.y = Mathf.Sqrt(6f * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            questActive = !questActive;

            quest.SetActive(questActive);
            questGiver.questWindow(questActive);
            questGiver.ToggleCursorState();
        }


    }
}
