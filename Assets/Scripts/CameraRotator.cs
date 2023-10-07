using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public Transform planet; // Reference to the planet GameObject
    public float rotationSpeed = 2.0f; // Adjust the rotation speed as needed

    private Vector3 previousMousePosition;

    void Start()
    {
        previousMousePosition = Input.mousePosition;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - previousMousePosition;

            // Rotate the camera around the planet based on mouse movement
            transform.RotateAround(planet.position, Vector3.up, mouseDelta.x * rotationSpeed);
            transform.RotateAround(planet.position, transform.right, -mouseDelta.y * rotationSpeed);
        }

        previousMousePosition = Input.mousePosition;
    }
}