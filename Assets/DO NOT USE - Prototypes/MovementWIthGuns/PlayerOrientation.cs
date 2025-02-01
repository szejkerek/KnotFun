using UnityEngine;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerOrientation : MonoBehaviour
    {
        public GameObject orientation;
        public GameObject playerModel; // Add reference to the player model
        private Vector2 lookVector = Vector2.zero;
        public float rotationSpeed = 5f; // Rotation speed for smooth lerping

        private void Start()
        {
            if (orientation == null)
            {
                Debug.LogError("Orientation GameObject is not assigned!");
            }

            if (playerModel == null)
            {
                Debug.LogError("Player model GameObject is not assigned!");
            }
        }

        private void Update()
        {
            RotateOrientationTowardsLookVector();
            RotatePlayerModelTowardsOrientation();
        }

        public void SetLookVector(Vector2 look)
        {
            lookVector = look;
        }

        private void RotateOrientationTowardsLookVector()
        {
            if (lookVector != Vector2.zero && orientation != null)
            {
                Vector3 lookDirection = new Vector3(lookVector.x, 0f, lookVector.y).normalized;
                orientation.transform.forward = lookDirection;
            }
        }

        private void RotatePlayerModelTowardsOrientation()
        {
            if (playerModel != null && orientation != null)
            {
                // Get the target rotation based on the orientation's forward direction
                Quaternion targetRotation = Quaternion.LookRotation(orientation.transform.forward);

                // Smoothly rotate the player model towards the target rotation
                playerModel.transform.rotation = Quaternion.Lerp(
                    playerModel.transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }

        private void OnDrawGizmos()
        {
            if (orientation != null && lookVector != Vector2.zero)
            {
                Gizmos.color = Color.green;
                Vector3 position = orientation.transform.position;
                Vector3 lookDirection = new Vector3(lookVector.x, 0f, lookVector.y).normalized;

                Gizmos.DrawLine(position, position + lookDirection * 2f);
                Gizmos.DrawSphere(position + lookDirection * 2f, 0.1f);
            }
        }
    }
}
