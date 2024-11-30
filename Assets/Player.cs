using GogoGaga.OptimizedRopesAndCables;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.XInput;
 

public class Player : MonoBehaviour
{
    public Vector3 currentDirection { get; set; }
    private CharacterController characterController;
    public PlayerAttackManager PlayerAttackManager {get; private set;}

    [SerializeField]
    public int playerNumber = 1;

    public ReadOnlyArray<Gamepad> gamepads = Gamepad.all;

    //input z kontrollera

    public Rope PlayerRope;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
<<<<<<< Updated upstream
        PlayerAttackManager = GetComponent<PlayerAttackManager>();
=======
        gamepads = Gamepad.all;

>>>>>>> Stashed changes
    }

    public void Move()
    {
        characterController.Move(currentDirection);
    }



    public Vector3 GetMovementDirection(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {

        if(playerNumber == 1)
        {
            Vector3 moveDirection = Vector3.zero;
            Vector2 moveInput = gamepads[0].leftStick.ReadValue().normalized;
            moveDirection += new Vector3(moveInput.x, 0f, moveInput.y);
            return moveDirection;
        }

        else if (playerNumber == 2)
        {
            Vector3 moveDirection = Vector3.zero;
            Vector2 moveInput = gamepads[1].leftStick.ReadValue().normalized;
            moveDirection += new Vector3(moveInput.x, 0f, moveInput.y);
            return moveDirection;
        }

        else
        {
            Vector3 moveDirection = Vector3.zero;
            if (Input.GetKey(up)) moveDirection += Vector3.forward;
            if (Input.GetKey(down)) moveDirection += Vector3.back;
            if (Input.GetKey(left)) moveDirection += Vector3.left;
            if (Input.GetKey(right)) moveDirection += Vector3.right;

            return moveDirection;
        }

    }

    public void Update()
    {
        if(gamepads[0].rightTrigger.ReadValue() > 0.1f)
        {
            Debug.Log("Prawy trigger 1 gracz");
        }
        if (gamepads[1].rightTrigger.ReadValue() > 0.1f)
        {
            Debug.Log("Prawy trigger 2 gracz");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Spacja 3 gracz");
        }
    }



    //czy jest klikany trigger albo spacja 
}
