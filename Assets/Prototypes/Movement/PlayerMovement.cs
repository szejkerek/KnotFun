using System;
using Unity.VisualScripting;
using UnityEngine;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerOrientation PlayerOrientation { get; private set; }
        
        [SerializeField] private int playerIndex = -1;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float groundDrag = 3f;

        private Vector2 inputVector = Vector2.zero;
        
        [Header("Ground Check")]
        public LayerMask whatIsGround;
        public float playerHeight;
        bool grounded;
        
        
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
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
            
            SpeedControl();
            rb.linearDamping = grounded ? groundDrag : 0f;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            if (inputVector == Vector2.zero)
                return;

            Vector3 flatMovement = new Vector3(inputVector.x, 0f, inputVector.y);
            rb.AddForce(flatMovement.normalized * (speed * 10f), ForceMode.Force);
        }

        void SpeedControl()
        {
            Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

            if (flatVelocity.magnitude > speed)
            {
                Vector3 newVelocity = flatVelocity.normalized * speed;
                rb.linearVelocity = new Vector3(newVelocity.x, rb.linearVelocity.y, newVelocity.z);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = grounded ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position + Vector3.down * (playerHeight * 0.5f + 0.2f), 0.1f);
        }
    }
}