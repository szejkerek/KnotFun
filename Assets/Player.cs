using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 currentDirection { get; set; }
    private CharacterController characterController;

    public Rope PlayerRope;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Move()
    {
        characterController.Move(currentDirection);
    }

    public Vector3 GetMovementDirection(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(up)) moveDirection += Vector3.forward;
        if (Input.GetKey(down)) moveDirection += Vector3.back;
        if (Input.GetKey(left)) moveDirection += Vector3.left;
        if (Input.GetKey(right)) moveDirection += Vector3.right;

        return moveDirection;
    }
}
