using UnityEngine;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerCameraCenter : MonoBehaviour
    {
        private PlayerMovement[] players;
        private GameObject[] orientations;

        [SerializeField, Range(0f, 1f)] private float positionLerpFactor = 0.1f;
        [SerializeField, Range(0f, 1f)] private float rotationLerpFactor = 0.05f;

        private void Awake()
        {
            players = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
            orientations = new GameObject[players.Length];
            for (int i = 0; i < players.Length; i++)
            {
                orientations[i] = players[i].orientation;
            }

            // Set initial position and rotation without interpolation
            SetPosition(1.0f, 1.0f);
        }

        void Update()
        {
            SetPosition(positionLerpFactor, rotationLerpFactor);
        }

        private void SetPosition(float positionLerpTime, float rotationLerpTime)
        {
            if (players.Length == 0) return;

            // Calculate average position and generalized forward direction
            Vector3 avgPosition = Vector3.zero;
            Vector3 avgDirection = Vector3.zero;

            foreach (var player in players)
            {
                avgPosition += player.transform.position;
            }

            foreach (var orientation in orientations)
            {
                avgDirection += orientation.transform.forward;
            }

            avgPosition /= players.Length;
            avgDirection.Normalize();

            // Smoothly interpolate position and set rotation
            transform.position = Vector3.Lerp(transform.position, avgPosition, positionLerpTime);

            Quaternion targetRotation = Quaternion.LookRotation(avgDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationLerpTime);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying || players.Length == 0) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.2f);

            Gizmos.color = Color.green;
            Vector3 forwardDirection = transform.forward * 2f;
            Gizmos.DrawLine(transform.position, transform.position + forwardDirection);
        }
    }
}
