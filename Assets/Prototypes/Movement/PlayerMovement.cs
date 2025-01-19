using System;
using UnityEngine;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public GameObject orientation;
        [SerializeField] private int playerIndex = -1;
        [SerializeField] private float speed = 3f;

        private Vector3 moveDirection = Vector3.zero;
        private Vector2 inputVector = Vector2.zero;
        private Vector2 lookVector = Vector2.zero;

        private void Start()
        {
            if (orientation == null)
            {
                Debug.LogError("Orientation GameObject is not assigned!");
            }
        }

        private void Update()
        {
            RotateOrientationTowardsLookVector();
        }

        public int GetPlayerIndex()
        {
            return playerIndex;
        }

        public void SetInputVector(Vector2 input)
        {
            inputVector = input;
        }

        public void SetLookVector(Vector2 look)
        {
            lookVector = look;
        }

        private void RotateOrientationTowardsLookVector()
        {
            if (lookVector != Vector2.zero)
            {
                // Convert lookVector (2D) to a 3D direction.
                Vector3 lookDirection = new Vector3(lookVector.x, 0f, lookVector.y);

                // Rotate the orientation GameObject to face the look direction.
                orientation.transform.forward = lookDirection;
            }
        }

        private void OnDrawGizmos()
        {
            if (orientation != null && lookVector != Vector2.zero)
            {
                // Draw a line to represent the orientation's look direction.
                Gizmos.color = Color.green;
                Vector3 position = orientation.transform.position;
                Vector3 lookDirection = new Vector3(lookVector.x, 0f, lookVector.y).normalized;

                Gizmos.DrawLine(position, position + lookDirection * 2f);
                Gizmos.DrawSphere(position + lookDirection * 2f, 0.1f);
            }
        }
    }
}
