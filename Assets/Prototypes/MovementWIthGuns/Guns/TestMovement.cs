using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolders
{
    public class TestMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 5f;
        public float mouseSensitivity = 2000f;

        [Header("Camera Settings")]
        public Transform cameraTransform;

        [Header("Weapon Settings")]
        public List<GameObject> weapons;
        private int currentWeaponIndex = 0;

        private float xRotation = 0f;

        void Start()
        {
            if (cameraTransform == null)
            {
                Debug.LogError("Camera Transform is not assigned. Please assign it in the inspector.");
                return;
            }

            if (weapons.Count > 0)
            {
                ActivateWeapon(currentWeaponIndex);
            }

            // Lock the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            HandleMouseLook();
            HandleMovement();
            HandleWeaponSwitch();
        }

        private void HandleMouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the player horizontally
            transform.Rotate(Vector3.up * mouseX);

            // Rotate the camera vertically
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        private void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = transform.right * horizontal + transform.forward * vertical;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        private void HandleWeaponSwitch()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
                ActivateWeapon(currentWeaponIndex);
            }
        }

        private void ActivateWeapon(int index)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(i == index);
            }
        }
    }


}
