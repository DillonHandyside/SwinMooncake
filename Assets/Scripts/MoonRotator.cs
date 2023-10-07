using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotator : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 10.0f;
    public float smoothness = 10.0f;

    private Vector3 targetEulerAngles;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        targetEulerAngles = target.eulerAngles;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform == target)
            {
                targetEulerAngles = target.eulerAngles;
            }
        }

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed;

            targetEulerAngles.x += mouseY;
            targetEulerAngles.y += mouseX;

            // Clamp the pitch rotation to avoid flipping
            targetEulerAngles.x = Mathf.Clamp(targetEulerAngles.x, -90.0f, 90.0f);

            //
            target.rotation = Quaternion.Lerp(target.rotation, Quaternion.Euler(targetEulerAngles), Time.deltaTime * smoothness);
        }
    }
}