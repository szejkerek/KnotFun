using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput playerInput;
        private PlayerMovement playerMovement;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();

            if (playerInput != null)
            {
                // Find the matching PlayerMovement instance based on playerIndex
                PlayerMovement[] movements = FindObjectsOfType<PlayerMovement>();
                playerMovement = System.Array.Find(movements,
                    movement => movement.GetPlayerIndex() == playerInput.playerIndex);
            }

            if (playerMovement == null)
            {
                Debug.LogWarning($"No PlayerMovement found for PlayerIndex: {playerInput.playerIndex}");
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (playerMovement != null)
            {
                playerMovement.SetInputVector(context.ReadValue<Vector2>());
            }
        }
    }
}
