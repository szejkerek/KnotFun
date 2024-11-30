using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 3f;

    [SerializeField]
    private int playerIndex = 0;

    private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 inputVector = Vector3.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }


    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    void Update()
    {
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= MoveSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        // Optionally clamp the positions to keep pads within bounds
        //ClampPadPosition(player1);
        //ClampPadPosition(player2);
    }

    void ClampPadPosition(Transform pad)
    {
        Vector3 pos = pad.position;
        pos.y = Mathf.Clamp(pos.y, -4.5f, 4.5f); // Adjust bounds based on your scene
        pad.position = pos;
    }
}
