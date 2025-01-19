using UnityEngine;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerOrientation : MonoBehaviour
    {
        public GameObject orientation;
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

        public void SetLookVector(Vector2 look)
        {
            lookVector = look;
        }

        private void RotateOrientationTowardsLookVector()
        {
            if (lookVector != Vector2.zero && orientation != null)
            {
                Vector3 lookDirection = new Vector3(lookVector.x, 0f, lookVector.y);
                orientation.transform.forward = lookDirection;
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