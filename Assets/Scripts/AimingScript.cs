using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingScript : MonoBehaviour
{
    public GameObject Gun;
    public Camera mainCamera;  

    public float zoomedFOV = 30f;
    private float normalFOV;  
    public float zoomSpeed = 2f;  // Adjust this value for faster or slower zoom transitions

    void Start()
    {
        normalFOV = mainCamera.fieldOfView;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Gun.GetComponent<Animator>().Play("Aim");
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomedFOV, zoomSpeed * Time.deltaTime);
        }
        else
        {
            Gun.GetComponent<Animator>().Play("Descope");
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFOV, zoomSpeed * Time.deltaTime);
        }
    }
}
