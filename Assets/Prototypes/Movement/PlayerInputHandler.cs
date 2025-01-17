using UnityEngine;
using UnityEngine.InputSystem;

namespace PlaceHolders
{
    public class PlayerInputHandler : MonoBehaviour
    {
        PlayerInput playerInput;
        PlayerMovement playerMovement;
        void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerInput = GetComponent<PlayerInput>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            playerMovement.SetInputVector(context.ReadValue<Vector2>());
        }
        
    }
}
