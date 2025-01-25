using UnityEngine;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerCameraCenter : MonoBehaviour
    {
        private PlayerOrientation[] players;
        private GameObject[] orientations;

        [SerializeField, Range(0f, 1f)] private float positionLerpFactor = 0.1f;
        [SerializeField, Range(0f, 1f)] private float rotationLerpFactor = 0.05f;

        private void Awake()
        {
            players = FindObjectsByType<PlayerOrientation>(FindObjectsSortMode.None);
            orientations = new GameObject[players.Length];
            for (int i = 0; i < players.Length; i++)
            {
                orientations[i] = players[i].orientation;
            }

            SetPosition(1.0f, 1.0f);
        }

        void Update()
        {
            SetPosition(positionLerpFactor, rotationLerpFactor);
        }

        private void SetPosition(float positionLerpTime, float rotationLerpTime)
        {
            if (players.Length == 0) return;
            
            Vector3 avgPosition = Vector3.zero;
            foreach (var player in players)
            {
                avgPosition += player.transform.position;
            }
            avgPosition /= players.Length;

            transform.position = Vector3.Lerp(transform.position, avgPosition, positionLerpTime);

            Quaternion avgRotation = CalculateAverageRotation();

            Quaternion targetRotation = Quaternion.LookRotation(avgRotation * Vector3.forward, Vector3.up);
            float currentYaw = transform.eulerAngles.y;
            float targetYaw = targetRotation.eulerAngles.y;

            if (targetYaw < currentYaw) targetYaw += 360;
            float newYaw = Mathf.LerpAngle(currentYaw, targetYaw, rotationLerpTime);

            transform.rotation = Quaternion.Euler(0, newYaw, 0);
        }

        private Quaternion CalculateAverageRotation()
        {
            if (orientations.Length == 0) return Quaternion.identity;

            Quaternion avgRotation = orientations[0].transform.rotation;
            for (int i = 1; i < orientations.Length; i++)
            {
                Quaternion currentRotation = orientations[i].transform.rotation;

                if (Quaternion.Dot(avgRotation, currentRotation) < 0)
                {
                    currentRotation = new Quaternion(-currentRotation.x, -currentRotation.y, -currentRotation.z, -currentRotation.w);
                }

                float weight = 1.0f / (i + 1);
                avgRotation = Quaternion.Slerp(avgRotation, currentRotation, weight);
            }
            return avgRotation;
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
