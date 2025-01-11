using UnityEngine;

public class MarkerX : MonoBehaviour
{
    public Transform cameraTransform; // Drag the Camera object into this field in the Inspector
    public float bounceAmplitude = 0.5f; // The height of the bounce
    public float bounceFrequency = 2f;  // The speed of the bounce
    public Transform playerTransform;  // The player's Transform to follow

    private Vector3 initialOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(Transform player)
    {
        playerTransform = player;

        // Save the offset from the player's position
        initialOffset = transform.position - playerTransform.position;

        // Automatically find the main camera if not set
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null) return;

        // Update the position to follow the player with the initial offset
        Vector3 targetPosition = playerTransform.position + initialOffset;

        // Bounce up and down
        float bounce = Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;
        targetPosition.y += bounce;

        transform.position = targetPosition;

        // Rotate to face the camera
        Vector3 directionToCamera = cameraTransform.position - transform.position;
        directionToCamera.y = 0; // Keep rotation only on the Y-axis
        transform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}