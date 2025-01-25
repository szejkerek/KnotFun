using UnityEngine;
using System.Collections;

namespace PlaceHolders
{
    public class TetheredEnemies : MonoBehaviour
    {
        public GameObject otherEnemy; 
        public float detectionRadius = 20f;
        public float speed = 5f;
        private Vector3 midpoint; 
        private Vector3 directionToPlayer; 
        private bool playerDetected = false;
        public float moveDuration = 5f;
        public float stopDuration = 3f;
        private bool isStopped = false; 
        private float stopTimer = 0f;
        private float moveTimer = 0f; 

        private GameObject player;    

        void Start()
        {
           
            if (otherEnemy == null)
            {
                Debug.LogError("Brak przypisanego obiektu otherEnemy!");
            } 

        }

        void Update()
        {
            if (isStopped)
            {
                HandleStop();
            }
            else if (!playerDetected)
            {
                DetectPlayer();
            }
            else
            {
                MoveEnemies();
            }
        }

        void DetectPlayer()
        {
            midpoint = (transform.position + otherEnemy.transform.position) / 2f;

            RaycastHit[] hits = Physics.SphereCastAll(midpoint, detectionRadius, Vector3.up, 0f);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    player = hit.collider.gameObject;

                    directionToPlayer = (player.transform.position - midpoint).normalized;

                    playerDetected = true; 
                    Debug.Log($"Znaleziono obiekt z tagiem 'Player': {player.name}");
                    break;
                }
            }
        }


        void MoveEnemies()
        {
            moveTimer += Time.deltaTime;

            float t = moveTimer / moveDuration;

            float easeSpeed = speed * EaseInOut(t);

            if (moveDuration - moveTimer <= 2f)
            {
                float slowDownFactor = (moveDuration - moveTimer) / 2f;
                easeSpeed *= slowDownFactor; 
            }

            Vector3 newPosition = transform.position + directionToPlayer * easeSpeed * Time.deltaTime;
            Vector3 otherNewPosition = otherEnemy.transform.position + directionToPlayer * easeSpeed * Time.deltaTime;

            Vector3 offset = otherEnemy.transform.position - transform.position;

            transform.position = newPosition;
            otherEnemy.transform.position = newPosition + offset;

            if (moveTimer >= moveDuration)
            {
                StopEnemies();
            }
        }

        float EaseInOut(float t)
        {
            t = Mathf.Clamp01(t);

            return t * t * (3f - 2f * t);
        }


        void StopEnemies()
        {
            isStopped = true;
            stopTimer = 0f; 
            playerDetected = false; 
        }

        void HandleStop()
        {
            stopTimer += Time.deltaTime;

            if (stopTimer >= stopDuration)
            {
                isStopped = false;
                playerDetected = false; 
                moveTimer = 0f;      
            }
        }

        private void OnDrawGizmos()
        {
            if (otherEnemy != null)
            {
                Vector3 midpoint = (transform.position + otherEnemy.transform.position) / 2f;

                Gizmos.color = Color.red; 
                Gizmos.DrawWireSphere(midpoint, detectionRadius); 
            }
        }

    }
}

