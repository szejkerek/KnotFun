using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerInputHandler : MonoBehaviour
    {
        PlayerInput playerInput;
        PlayerMovement playerMovement;
        PlayerOrientation playerOrientation;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();

            if (playerInput != null)
            {
                PlayerMovement[] movements = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
                playerMovement = System.Array.Find(movements,
                    movement => movement.GetPlayerIndex() == playerInput.playerIndex);

                playerOrientation = playerMovement.PlayerOrientation;
            }

            if (playerMovement == null)
            {
                Debug.LogWarning($"No PlayerMovement found for PlayerIndex: {playerInput.playerIndex}");
            }
        }
        
        public void OnLook(InputAction.CallbackContext context)
        {
            if (playerOrientation == null)
                return;
            
            playerOrientation.SetLookVector(context.ReadValue<Vector2>());
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (playerMovement == null)
                return;
            
            playerMovement.SetInputVector(context.ReadValue<Vector2>());
        }
    }
}
