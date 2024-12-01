using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;


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


    bool dead = false;
    bool isShooting = false;

    public Transform source;
    LineRenderer lineRenderer;

    Animator animator;
    SkinnedMeshRenderer skinnedMeshRenderer;

    public List<Sound> sounds;
    public AudioManager audioManager;
    public AudioSource audioSource;
    public AudioSource audioSourceWalk;
    private double nextStartTime;

    public float height = 1f;
    public float length = 50f;

    private bool isGrounded;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        PlayerAttackManager = GetComponent<PlayerAttackManager>();
        animator = GetComponentInChildren<Animator>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        lineRenderer = GetComponentInChildren<LineRenderer>();

        lineRenderer.material.SetColor("_EmissionColor", GetMainMaterial().GetColor("_EmissionColor") * 3);

        skinnedMeshRenderer.material.SetColor("_EmissionColor", GetMainMaterial().GetColor("_EmissionColor") * 3);

        gamepads = Gamepad.all;
    }

    public void Move()
    {
        if (Mathf.Abs(currentDirection.x) + Mathf.Abs(currentDirection.z) > 0.01f)
        {
            animator.SetBool("IsMoving", true);
            transform.LookAt(transform.position + new Vector3(currentDirection.x, 0, currentDirection.z));
            if (!audioSourceWalk.isPlaying)
                audioSourceWalk.Play();
        }
        else
        {
            animator.SetBool("IsMoving", false);
            audioSourceWalk.Stop();
        }
        characterController.Move(currentDirection);

        isShooting = TriggerHeld(gameDevice);
        animator.SetBool("IsShooting", isShooting);

        Vector3 targetPosition = (transform.position + transform.rotation * Vector3.forward * length + Vector3.up * height).normalized * length;
        if (isShooting)
        {
            if (AudioSettings.dspTime == nextStartTime)
            {
                audioSource.PlayScheduled(nextStartTime);
                nextStartTime += audioSource.clip.length;
            }

            if (!audioSource.isPlaying)
            { audioSource.Play(); }

            RaycastHit hit;
            if (Physics.Raycast(source.transform.position, (transform.position + transform.rotation * Vector3.forward * length + Vector3.up * height).normalized, out hit, length))
            {
                targetPosition = hit.point;
            }
        }
        else
        {
            audioSource.Stop();
        }

        lineRenderer.enabled = isShooting;
        lineRenderer.SetPosition(0, source.transform.position);
        lineRenderer.SetPosition(1, targetPosition);

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
                return (gamepads[0].rightTrigger.IsPressed() || gamepads[0].leftTrigger.IsPressed() || gamepads[0].aButton.IsPressed() || gamepads[0].xButton.IsPressed());
            case GameDevice.Pad2:
                return (gamepads[1].rightTrigger.IsPressed() || gamepads[1].leftTrigger.IsPressed() || gamepads[1].aButton.IsPressed() || gamepads[1].xButton.IsPressed());
            case GameDevice.Keyboard:
                return Input.GetKey(KeyCode.Space);
        }
        
        return false;
    }
}
