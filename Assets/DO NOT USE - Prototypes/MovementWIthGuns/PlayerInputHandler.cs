using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlaceHolders.Prototypes.Movement
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 3f)] private float padDeadZone = 0.6f;
        
        private PlayerInput playerInput;
        private PlayerMovement playerMovement;
        private PlayerOrientation playerOrientation;
        private PlayerGuns playerGuns;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            
            if (playerInput != null)
            {
                PlayerMovement[] movements = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
                playerMovement = System.Array.Find(
                    movements,
                    movement => movement.GetPlayerIndex() == playerInput.playerIndex
                );
                
                if (playerMovement != null)
                {
                    playerOrientation = playerMovement.PlayerOrientation;
                    playerGuns = playerMovement.GetComponent<PlayerGuns>();
                }
            }

            if (playerMovement == null)
            {
                Debug.LogWarning($"No PlayerMovement found for PlayerIndex: {playerInput.playerIndex}");
            }

            if (playerGuns == null)
            {
                Debug.LogWarning($"No PlayerGuns found for PlayerIndex: {playerInput.playerIndex}");
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (playerOrientation == null) return;
            
            Vector2 input = context.ReadValue<Vector2>();
            
            if (input.sqrMagnitude < padDeadZone * padDeadZone)
                input = Vector2.zero;

            playerOrientation.SetLookVector(input);
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            if (playerMovement == null) return;
            if (context.started)
            {
                playerMovement.TryTriggerJump();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (playerMovement == null) return;

            Vector2 input = context.ReadValue<Vector2>();

            // Dead zone for gamepad or any analog input
            if (input.sqrMagnitude < padDeadZone * padDeadZone)
                input = Vector2.zero;

            playerMovement.SetInputVector(input);
        }
        
        public void OnNext(InputAction.CallbackContext context)
        {
            if (playerGuns == null) return;
            if (context.performed) // only trigger once at the correct phase
            {
                playerGuns.OnScrollUp();
            }
        }
        
        public void OnPrevious(InputAction.CallbackContext context)
        {
            if (playerGuns == null) return;
            if (context.performed)
            {
                playerGuns.OnScrollDown();
            }
        }
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (playerGuns == null) return;
            if (context.performed)
            {
                playerGuns.OnUse();
            }
        }
    }
}
