using System.Collections;
using UnityEngine;

namespace PlaceHolders
{
    public class MinePlanter : MonoBehaviour
    {
        [SerializeField]
        private float minDistance = 5f, waitTime = 0.2f, bombPlantTime = 1f;

        [SerializeField]
        private Transform player;

        [SerializeField]
        private float jumpForce = 50f, jumpHeight = 3f, bias = 1f;

        [SerializeField]
        private GameObject bomb;

        [SerializeField]
        private Sensor floorSensor;

        private Rigidbody rb;
        private float lastActionTime = 0f;
        private bool isPlantingBomb = false;
        private bool isWaiting = false;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();

            // Ensure dependencies are assigned
            if (player == null || floorSensor == null || bomb == null)
            {
                Debug.LogError("Dependencies not assigned in MinePlanter script.");
            }
        }

        private void Update()
        {
            if (floorSensor != null && floorSensor.activated && Time.time >= lastActionTime)
            {
                if (!isPlantingBomb && !isWaiting)
                {
                    if (Vector3.Distance(transform.position, player.position) < minDistance)
                    {
                        StartPlantBomb();
                    }
                    else
                    {
                        StartWait();
                    }
                }

                if (isWaiting && Time.time >= lastActionTime + waitTime)
                {
                    Jump();
                    isWaiting = false;
                }

                if (isPlantingBomb && Time.time >= lastActionTime + bombPlantTime)
                {
                    PlantBomb();
                    Jump();
                    isPlantingBomb = false;
                }
            }
        }

        private void StartWait()
        {
            Debug.Log("Starting wait...");
            isWaiting = true;
            lastActionTime = Time.time;
        }

        private void StartPlantBomb()
        {
            Debug.Log("Starting to plant bomb...");
            isPlantingBomb = true;
            lastActionTime = Time.time;
        }

        private void PlantBomb()
        {
            Debug.Log("Planting bomb...");

            if (bomb != null && floorSensor != null)
            {
                Instantiate(bomb, floorSensor.transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Bomb or floorSensor is null while planting.");
            }
        }

        private void Jump()
        {
            Debug.Log("Jumping...");

            if (rb == null || player == null)
            {
                Debug.LogError("Rigidbody or player is null in Jump method.");
                return;
            }
            
            Vector3 jumpDirection = player.position - rb.position;
            if (jumpDirection.magnitude < minDistance)
            {
                jumpDirection = -jumpDirection;
            }

            float xBias = Random.Range(-bias, bias);
            float zBias = Random.Range(-bias, bias);
            jumpDirection = (jumpDirection.normalized + new Vector3(xBias, 0, zBias)).normalized;

            jumpDirection += Vector3.up * jumpHeight;

            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        }
    }
}
