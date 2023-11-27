
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayersMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 2.0f;

    public Camera freeLookCam;
    private float verticalRotation = 0f;

    void Start()
    {
        //freeLookCam = FindObjectOfType<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
  
        float horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        Vector3 movement = transform.TransformDirection(new Vector3(horizontalMovement, 0, verticalMovement));
        transform.Translate(movement);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation += mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }
}
