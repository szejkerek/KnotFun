using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


public class inputSystem: MonoBehaviour
{
    private PlayerInput playerInput;
    private TwoPlayerMovement movement;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<TwoPlayerMovement>();
        var index = playerInput.playerIndex;
        movement = players.FirstOrDefault(m => m.GetPlayerIndex() == index);

    }

    public void OnMove(CallbackContext context)
    {
        if(movement != null)
            movement.SetInputVector(context.ReadValue<Vector2>());
    }

}