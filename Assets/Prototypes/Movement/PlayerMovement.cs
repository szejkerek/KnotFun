using System;
using UnityEngine;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerOrientation PlayerOrientation { get; private set; }
        
        [SerializeField] private int playerIndex = -1;
        [SerializeField] private float speed = 3f;

        private Vector2 inputVector = Vector2.zero;
        
        private void Awake()
        {
            PlayerOrientation = GetComponent<PlayerOrientation>();
        }

        public int GetPlayerIndex()
        {
            return playerIndex;
        }

        public void SetInputVector(Vector2 input)
        {
            inputVector = input;
        }

        private void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            if (inputVector != Vector2.zero)
            {
                Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
                transform.position += moveDirection * speed * Time.deltaTime;
            }
        }
    }
}