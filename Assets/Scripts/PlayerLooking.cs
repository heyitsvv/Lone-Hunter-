using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLooking : MonoBehaviour
{
    public float X_sensitivity = 300f;
    public float Y_sensitivity = 300f;
    public Transform Player;

    private float xRotation = 0f;
    private float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Capture the mouse input
        mouseX = Input.GetAxisRaw("Mouse X") * X_sensitivity * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * Y_sensitivity * Time.deltaTime;

        // Calculate the x rotation (vertical looking) but don't apply it yet
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    // LateUpdate is called after all Update methods
    void LateUpdate()
    {
        // Apply the x rotation (vertical looking)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apply the y rotation (horizontal looking) around the Y axis
        Player.Rotate(Vector3.up * mouseX);
    }
}

