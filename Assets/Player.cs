using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public Transform scope;

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
    public float dps = 30f;

    

    public float rotationSpeed = 10f;

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

    private bool isCurrentlyActive = false;

    public void Move()
    {
        // Determine if the character is active
        bool isMovingOrShooting = GetMovementDirection() != Vector3.zero || TriggerHeld(gameDevice);

        if (isMovingOrShooting != isCurrentlyActive)
        {
            isCurrentlyActive = isMovingOrShooting;
            BulletTimeManager.Instance.SetCharacterActive(isCurrentlyActive);
        }
        
        if (dead) return;
        

        if (Mathf.Abs(currentDirection.x) + Mathf.Abs(currentDirection.z) > 0.01f)
        {
            animator.SetBool("IsMoving", true);
            //transform.LookAt(transform.position + new Vector3(currentDirection.x, 0, currentDirection.z));
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

        if (PlayerAttackManager.currentCharge == 0)
        {
            lineRenderer.enabled = false;
            audioSource.Stop();
            animator.SetBool("IsShooting", false);
            return;
        }
        
        animator.SetBool("IsShooting", isShooting);

        Vector3 targetPosition = scope.position + scope.forward * length;
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
            if (Physics.Raycast(scope.position, scope.forward, out hit, length))
            {
                targetPosition = hit.point;
                if(hit.collider.gameObject.layer == 8)
                {
                    EnemyHealth enemyHealth;
                    if(hit.collider.gameObject.TryGetComponent<EnemyHealth>(out enemyHealth))
                    {
                        enemyHealth.DecreaseHealth(Time.deltaTime * dps);
                    }
                }
                else if (hit.collider.gameObject.layer == 9)
                {
                    hit.collider.gameObject.GetComponentInChildren<CentralCrystal>().AddColor(GetMainMaterial().GetColor("_EmissionColor"));
                }
            }
            
            
        }
        else
        {
            audioSource.Stop();
        }
        transform.LookAt(GetRotationDirection());

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


    public Vector3 GetRotationDirection()
    {
        if (dead) return Vector3.zero;
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
                    Vector3 rotation = Vector3.zero;
                    Vector2 rotationInput = gamepads[0].rightStick.ReadValue().normalized;
                    return transform.position + (Vector3.forward * rotationInput.y + Vector3.right * rotationInput.x);
                }
            case GameDevice.Pad2:
                {
                    Vector3 rotation = Vector3.zero;
                    Vector2 rotationInput = gamepads[1].rightStick.ReadValue().normalized;
                    return transform.position + (Vector3.forward * rotationInput.y + Vector3.right * rotationInput.x);
                }

            case GameDevice.Keyboard:
                {
                    Vector3 currentTarget = transform.position + transform.forward * 2;

                    float rotationInput = 0f;
                    if (Input.GetKey(KeyCode.Q))
                    {
                        rotationInput = -1f;
                    }
                    else if (Input.GetKey(KeyCode.E))
                    {
                        rotationInput = 1f;
                    }

                    if (rotationInput != 0)
                    {
                        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
                        Vector3 direction = Quaternion.Euler(0, rotationAmount, 0) * transform.forward;
                        currentTarget = transform.position + direction * 2;
                        
                    }
                    return currentTarget;

                }
            default:
                Debug.Log("GameDevice not set");
                return transform.forward;
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
        if (debugNoPads)
        {
            return Input.GetKey(KeyCode.Space);
        }
        
        switch (gameDevice)
        {
            case GameDevice.Pad1:
                return (gamepads[0].rightTrigger.IsPressed() || gamepads[0].leftTrigger.IsPressed() || gamepads[0].aButton.IsPressed() || gamepads[0].xButton.IsPressed());
            case GameDevice.Pad2:
                return (gamepads[1].rightTrigger.IsPressed() || gamepads[1].leftTrigger.IsPressed() || gamepads[1].aButton.IsPressed() || gamepads[1].xButton.IsPressed());
            case GameDevice.Keyboard:
                return Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
        }
        
        return false;
    }

    public void KillPlayer()
    {
        if (dead) return;
        lineRenderer.enabled = false;
        dead = true;
        animator.SetBool("IsDead", true);
        foreach (Player g in GameObject.FindObjectsByType<Player>(FindObjectsSortMode.None))
        {
            g.KillPlayer();
        }
        
    }
}
