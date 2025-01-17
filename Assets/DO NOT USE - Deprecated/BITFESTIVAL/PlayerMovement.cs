using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    CharacterController controller;

    Vector3 moveDirection = Vector3.zero;
    Vector2 inputVector = Vector2.zero;
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;
        controller.Move(moveDirection);
    }

    public void SetInputVector(Vector2 readValue)
    {
        inputVector = readValue;
    }
}
