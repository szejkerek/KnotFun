using UnityEngine;

namespace PlaceHolders.Prototypes.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public GameObject orientation;
        [SerializeField] private int playerIndex = -1;
        [SerializeField] private float speed = 3f;
        public int GetPlayerIndex()
        {
            return playerIndex;
        }
        
        private Vector3 moveDirection = Vector3.zero;
        private Vector2 inputVector = Vector2.zero;

        private void Awake()
        {

        }



        private void Update()
        {

        }

        
        public void SetInputVector(Vector2 input)
        {
            inputVector = input;
        }

    }
}