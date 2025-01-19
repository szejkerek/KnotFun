using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 3f)] private float padDeadZone = 0.6f;
        
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

            Vector2 input = context.ReadValue<Vector2>();

            if (input.sqrMagnitude < padDeadZone * padDeadZone)
                input = Vector2.zero;

            playerOrientation.SetLookVector(input);
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            if (playerMovement == null)
                return;

            playerMovement.TryTriggerJump();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (playerMovement == null)
                return;

            Vector2 input = context.ReadValue<Vector2>();

            if (input.sqrMagnitude < padDeadZone * padDeadZone)
                input = Vector2.zero;

            playerMovement.SetInputVector(input);
        }
    }
}
