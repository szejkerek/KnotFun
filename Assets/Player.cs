using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class Player : MonoBehaviour
{

    public Material pad1Mat;
    public Material pad2Mat;
    public Material keyboard;
    public bool debugNoPads = true;
    public Vector3 currentDirection { get; set; }
    private CharacterController characterController;
    public PlayerAttackManager PlayerAttackManager {get; private set;}
    public GameDevice gameDevice;
    
    public ReadOnlyArray<Gamepad> gamepads = Gamepad.all;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        PlayerAttackManager = GetComponent<PlayerAttackManager>();

        gamepads = Gamepad.all;

        GetComponent<MeshRenderer>().material = GetMainMaterial();
    }

    public void Move()
    {
        characterController.Move(currentDirection);
    }

    public Vector3 GetMovementDirection()
    {
        if (debugNoPads)
        {
            switch (gameDevice)
            {
                case GameDevice.Pad1:
                {
                    Vector3 moveDirection = Vector3.zero;
                    if (Input.GetKey(KeyCode.T)) moveDirection += Vector3.forward;
                    if (Input.GetKey(KeyCode.G)) moveDirection += Vector3.back;
                    if (Input.GetKey(KeyCode.F)) moveDirection += Vector3.left;
                    if (Input.GetKey(KeyCode.H)) moveDirection += Vector3.right;

                    return moveDirection;
                }
                case GameDevice.Pad2:
                {
                    Vector3 moveDirection = Vector3.zero;
                    if (Input.GetKey(KeyCode.I)) moveDirection += Vector3.forward;
                    if (Input.GetKey(KeyCode.K)) moveDirection += Vector3.back;
                    if (Input.GetKey(KeyCode.J)) moveDirection += Vector3.left;
                    if (Input.GetKey(KeyCode.L)) moveDirection += Vector3.right;

                    return moveDirection;
                }

                case GameDevice.Keyboard:
                {
                    Vector3 moveDirection = Vector3.zero;
                    if (Input.GetKey(KeyCode.W)) moveDirection += Vector3.forward;
                    if (Input.GetKey(KeyCode.S)) moveDirection += Vector3.back;
                    if (Input.GetKey(KeyCode.A)) moveDirection += Vector3.left;
                    if (Input.GetKey(KeyCode.D)) moveDirection += Vector3.right;

                    return moveDirection;

                }
                default:
                    Debug.Log("GameDevice not set");
                    return Vector3.zero;
            }
        }

        switch (gameDevice)
        {
            case GameDevice.Pad1:
            {
                Vector3 moveDirection = Vector3.zero;
                Vector2 moveInput = gamepads[0].leftStick.ReadValue().normalized;
                moveDirection += new Vector3(moveInput.x, 0f, moveInput.y);
                return moveDirection;
            }
            case GameDevice.Pad2:
            {
                Vector3 moveDirection = Vector3.zero;
                Vector2 moveInput = gamepads[1].leftStick.ReadValue().normalized;
                moveDirection += new Vector3(moveInput.x, 0f, moveInput.y);
                return moveDirection;
            }

            case GameDevice.Keyboard:
            {
                Vector3 moveDirection = Vector3.zero;
                if (Input.GetKey(KeyCode.W)) moveDirection += Vector3.forward;
                if (Input.GetKey(KeyCode.S)) moveDirection += Vector3.back;
                if (Input.GetKey(KeyCode.A)) moveDirection += Vector3.left;
                if (Input.GetKey(KeyCode.D)) moveDirection += Vector3.right;

                return moveDirection;
            }
            default:
                Debug.Log("GameDevice not set");
                return Vector3.zero;
        }
        
        
    }

    public Material GetMainMaterial()
    {
        switch (gameDevice)
        {
            case GameDevice.Pad1:
                return pad1Mat;
            case GameDevice.Pad2:
                return pad2Mat;
            case GameDevice.Keyboard:
                return keyboard;
        }
        
        return null;
    }

    public bool TriggerHeld(GameDevice device)
    {
        switch (gameDevice)
        {
            case GameDevice.Pad1:
                break;
            case GameDevice.Pad2:
                break;
            case GameDevice.Keyboard:
                break;
        }
        
        return Input.GetKeyDown(KeyCode.Space);
    }
}
