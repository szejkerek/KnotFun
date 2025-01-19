using UnityEngine;
using Unity.Cinemachine;

namespace PlaceHolders
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        public Transform orientation;
        public Transform target;
        public CinemachineCamera cinemachineCam;

        private void Start()
        {
            if (cinemachineCam == null)
            {
                Debug.LogError("CinemachineVirtualCamera is not assigned.");
                return;
            }
        }

        private void LateUpdate()
        {
            if (orientation != null && target != null && cinemachineCam != null)
            {
                // Rotate the Cinemachine camera towards the orientation
                cinemachineCam.transform.rotation = Quaternion.Lerp(
                    cinemachineCam.transform.rotation,
                    orientation.rotation,
                    Time.deltaTime * 5f // Adjust smoothing factor as needed
                );

                // Make the camera look at the target
                cinemachineCam.LookAt = target;
            }
        }
    }
}