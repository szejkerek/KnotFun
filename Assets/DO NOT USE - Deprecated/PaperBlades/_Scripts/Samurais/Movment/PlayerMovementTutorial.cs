using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerMovementTutorial : MonoBehaviour
{
    const float gravity = -9.81f;
    public Vector3 MovementInputs { get; private set; }
    CharacterController characterController;

    float speed;


    [SerializeField] LayerMask groundMask;

    bool isGrounded;
    float groundCheckDistance = 0.4f;
    Transform groundCheck;

    Vector3 velocity;

    //MovementSchemes input = null;

    private void Awake()
    {
        //input = new MovementSchemes();
        characterController = GetComponent<CharacterController>();
        groundCheck = transform.Find("GroundCheck");
        if (groundCheck == null)
        {
            groundCheck = new GameObject("GroundCheck").transform;
            groundCheck.SetParent(transform);
            groundCheck.localPosition = Vector3.zero;
        }
    }

    private void Start()
    {
        speed = GetComponent<Samurai>().Character.GetStats().Speed;
    }

    //void OnEnable()
    //{
    //    input.Player3D.Enable();
    //    input.Player3D.Movement.performed += OnMovementPerformed;
    //    input.Player3D.Movement.canceled += OnMovementCancelled;
    //}
    //void OnDisable()
    //{
    //    input.Player3D.Disable();
    //    input.Player3D.Movement.performed -= OnMovementPerformed;
    //    input.Player3D.Movement.canceled -= OnMovementCancelled;
    //}

    //void OnMovementCancelled(InputAction.CallbackContext context) => MovementInputs = Vector3.zero;
    //void OnMovementPerformed(InputAction.CallbackContext context) => MovementInputs = context.ReadValue<Vector3>();



    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        MovementInputs.Normalize();

        characterController.Move(MovementInputs * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
