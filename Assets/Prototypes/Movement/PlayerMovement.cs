using UnityEngine;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private int playerIndex = -1; // Use camelCase for private variables
        [SerializeField] private float speed = 3f;
        private CharacterController controller;

        private Vector3 moveDirection = Vector3.zero;
        private Vector2 inputVector = Vector2.zero;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();

            if (controller == null)
            {
                Debug.LogError("CharacterController component is missing on this GameObject.");
            }
        }

        public int GetPlayerIndex()
        {
            return playerIndex;
        }

        private void Update()
        {
            if (controller != null)
            {
                moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
                moveDirection = transform.TransformDirection(moveDirection) * (speed * Time.deltaTime);
                controller.Move(moveDirection);
            }
        }

        public void SetInputVector(Vector2 input)
        {
            inputVector = input;
        }
    }
}