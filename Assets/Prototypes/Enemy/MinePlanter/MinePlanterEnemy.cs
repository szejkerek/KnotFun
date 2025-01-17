using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

namespace PlaceHolders
{
    public class MinePlanter : MonoBehaviour
    {
        [SerializeField]
        float minDistance = 5f, waitTime = 0.2f, bombPlantTime = 1f;
        [SerializeField]
        Transform player;
        [SerializeField]
        float jumpForce = 50f, jumpHeight = 3f, bias;
        Rigidbody rb;
        [SerializeField]
        GameObject bomb;
        [SerializeField]
        Sensor floorSensor;

        IEnumerator currentCoroutine = null;


        void Start ()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (floorSensor.activated && currentCoroutine == null)
            {
                if(Vector3.Distance(transform.position, player.position) < minDistance)
                {
                    currentCoroutine = Wait();
                }
                else currentCoroutine = PlantBomb();

                StartCoroutine(currentCoroutine);
            }
        }

        private IEnumerator PlantBomb()
        {
            Debug.Log("Planting");
            yield return new WaitForSeconds(bombPlantTime);
            Instantiate(bomb, floorSensor.transform.position, transform.rotation);
            Jump();
            currentCoroutine = null;
        }

        private IEnumerator Wait()
        {
            Debug.Log("Waiting");
            yield return new WaitForSeconds(waitTime);
            Jump();
            currentCoroutine = null;
        }

        private void Jump()
        {
            Debug.Log("Jumping");
            Vector3 jumpDirection = player.position - rb.position;

            if (jumpDirection.magnitude < minDistance){
                jumpDirection = - jumpDirection;
            }
            jumpDirection = jumpDirection.normalized;
            jumpDirection += Vector3.up * jumpHeight;
            jumpDirection = jumpDirection.normalized * jumpForce;

            rb.AddForce(jumpDirection);
        }

    }
}
