using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    public float startFOV = 60f;
    public float maxFOV = 100f;
    public float minFOV = 10f;
    public float zoomSpeed = 2f;
    public float smoothTime = 0.2f; // Smoothing time in seconds

    private Camera cameraComponent;
    private float targetFOV;

    private void Start()
    {
        cameraComponent = GetComponent<Camera>();
        cameraComponent.fieldOfView = startFOV;
        targetFOV = startFOV;
    }

    private void Update()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(zoomInput) > 0.01f)
        {
            // Determine the zoom direction based on the sign of zoomInput.
            float zoomDirection = Mathf.Sign(zoomInput);

            // Calculate the target FOV based on zoom direction.
            targetFOV = Mathf.Clamp(targetFOV + zoomDirection * zoomSpeed, minFOV, maxFOV);
        }

        // Smoothly interpolate towards the target FOV.
        float newFOV = Mathf.Lerp(cameraComponent.fieldOfView, targetFOV, smoothTime * Time.deltaTime);
        cameraComponent.fieldOfView = newFOV;
    }
}