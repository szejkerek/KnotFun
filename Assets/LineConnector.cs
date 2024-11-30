using UnityEngine;

public class LineConnector : MonoBehaviour
{
    public float hight; // The height of the orbit
    public Transform Target; // The target to face
    public float radius; // The radius of the orbit

    void Update()
    {
        if (transform.parent != null && Target != null)
        {
            Vector3 center = transform.parent.position;
            Vector3 directionToTarget = Target.position - center;
            
            directionToTarget.y = 0;
            
            directionToTarget.Normalize();
            
            Vector3 closestPoint = center + directionToTarget * radius;
            
            closestPoint.y = center.y + hight;
            transform.position = closestPoint;
            
            transform.LookAt(Target.position);
        }
    }
    
    void OnDrawGizmos()
    {
        if (transform.parent != null)
        {
            // Set the Gizmos color
            Gizmos.color = Color.red;

            // Get the center of the orbit
            Vector3 center = transform.parent.position;

            // Adjust the center to the correct height
            center.y += hight;

            // Draw the orbit as a circle in the XZ plane
            const int segments = 64; // Number of segments in the circle
            float angleStep = 360f / segments;

            Vector3 prevPoint = Vector3.zero;

            for (int i = 0; i <= segments; i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;

                Vector3 currentPoint = center + new Vector3(x, 0, z);

                if (i > 0)
                {
                    Gizmos.DrawLine(prevPoint, currentPoint);
                }

                prevPoint = currentPoint;
            }
        }
    }
}