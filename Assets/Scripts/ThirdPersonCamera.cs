using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Camera Settings")]
    [SerializeField] private float distance = 5f;
    [SerializeField] private float height = 1f;
    [SerializeField] private float sensitivity = 150f;

    [Header("Camera Smoothing")]
    [SerializeField] private float positionSmoothSpeed = 12f;
    [SerializeField] private float rotationSmoothSpeed = 12f;

    [Header("Vertical Limits")]
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 60f;

    private float yaw;
    private float pitch = 15f;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("ThirdPersonCamera: No target assigned!");
            enabled = false;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = target.eulerAngles.y;
    }

    private void LateUpdate()
    {
        HandleCameraRotation();
        UpdateCameraPosition();
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * sensitivity * Time.deltaTime;
        pitch -= mouseY * sensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    private void UpdateCameraPosition()
    {
        Quaternion orbitRotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 targetPosition = target.position;

        Vector3 desiredPosition =
            targetPosition +
            orbitRotation * new Vector3(0f, 0f, -distance);

        // Smooth camera position without excessive lag
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            positionSmoothSpeed * Time.deltaTime
        );

        Vector3 lookDirection = targetPosition - transform.position;

        if (lookDirection.sqrMagnitude > 0.001f)
        {
            Quaternion desiredRotation =
                Quaternion.LookRotation(lookDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                desiredRotation,
                rotationSmoothSpeed * Time.deltaTime
            );
        }
    }
}