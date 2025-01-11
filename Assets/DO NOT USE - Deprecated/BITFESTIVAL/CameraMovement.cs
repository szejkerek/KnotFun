using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float lowestCameraHeight = 5f; 
    public float highestCameraHeight = 20f; 
    public float transitionSpeed = 5f;

    public float interpolationMaxValue = 20f;

    public Vector3 baseCameraOffset = new Vector3(0f, 0f, -10f); // Default offset
    public float maxZOffset = -10f; // Z offset at highestCameraHeight
    public float minZOffset = -8f;  // Z offset at lowestCameraHeight

    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
    }

    private void Update()
    {
        // Calculate midpoint and average distance
        Vector3 pointAt = playerManager.GetMiddlePoint();
        float avgDist = playerManager.GetAvgDistance();

        // Calculate interpolated height and Z offset
        float targetHeight = Mathf.Lerp(lowestCameraHeight, highestCameraHeight, avgDist / interpolationMaxValue);
        float interpolatedZOffset = Mathf.Lerp(minZOffset, maxZOffset, avgDist / interpolationMaxValue);

        // Update the camera offset dynamically
        Vector3 cameraOffset = new Vector3(baseCameraOffset.x, baseCameraOffset.y, interpolatedZOffset);

        // Calculate the target position
        Vector3 targetPosition = new Vector3(pointAt.x, targetHeight, pointAt.z) + cameraOffset;

        // Move camera smoothly
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);

        // Make camera look at the midpoint
        transform.LookAt(pointAt);
    }

    private void OnDrawGizmos()
    {
        if (playerManager == null) return;

        // Draw the pointAt position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(playerManager.GetMiddlePoint(), 0.5f);

        // Draw a line from the camera to the pointAt position
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, playerManager.GetMiddlePoint());
    }
}
