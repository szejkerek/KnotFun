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
            if (!Application.isEditor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
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
                cinemachineCam.transform.rotation = Quaternion.Lerp(
                    cinemachineCam.transform.rotation,
                    orientation.rotation,
                    Time.deltaTime * 5f
                );
                cinemachineCam.LookAt = target;
            }
        }
    }
}