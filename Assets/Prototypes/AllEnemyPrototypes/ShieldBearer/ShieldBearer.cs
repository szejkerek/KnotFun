using UnityEngine;
using System.Linq;

namespace PlaceHolders
{
    public class ShieldBearer : MonoBehaviour
    {
        public float speed = 2f;
        public float rotationSpeed = 5f;
        public float protectionRadius = 4f;
        public LayerMask enemyLayer;

        private Transform targetPlayer;
        private Transform targetEnemy;
        private Rigidbody rb;
        private Vector3 debugDirection;
        private Vector3 debugShieldPosition;
        private bool onProtectionLine = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            FindNearestPlayer();
            FindNearestEnemy();

            if (targetPlayer != null && targetEnemy != null)
            {
                Vector3 bestPosition = GetShieldPosition();
                debugDirection = (bestPosition - transform.position).normalized;
                debugShieldPosition = bestPosition;
                MoveAndRotate(bestPosition);
            }
        }

        void FindNearestPlayer()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 0) return;
            targetPlayer = players.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).First().transform;
        }

        void FindNearestEnemy()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, protectionRadius, enemyLayer);
            if (enemies.Length == 1) return;
            targetEnemy = enemies.OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).ElementAt(1).transform;
        }

        Vector3 GetShieldPosition()
        {
            if (targetPlayer == null || targetEnemy == null)
                return transform.position;

            Vector3 directionToPlayer = (targetPlayer.position - targetEnemy.position).normalized;
            Vector3 linePosition = targetEnemy.position + directionToPlayer * 2f;
            linePosition.y = transform.position.y;

            if (!IsPointOnLineSegment(targetEnemy.position, targetPlayer.position,transform.position))
            {
                onProtectionLine = false;
                return ClosestPointOnLineSegment(targetEnemy.position, targetPlayer.position, transform.position); // Najpierw dociera do linii os³ony
            }
            else
            {
                onProtectionLine = true;
            }
            Debug.Log(transform.position);
            Debug.Log(targetPlayer.position);
            if (onProtectionLine && Vector3.Distance(transform.position, targetPlayer.position) > 2f)
            {
                return Vector3.MoveTowards(transform.position, targetPlayer.position, Vector3.Distance(transform.position, targetPlayer.position)- 2f);
            }

            return transform.position;
        }

        void MoveAndRotate(Vector3 targetPosition)
        {
            if (onProtectionLine && Vector3.Distance(transform.position, targetPlayer.position) <= 2f)
                return;

            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0;
            Debug.Log(direction);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime));
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + debugDirection * 2f);
            Gizmos.DrawSphere(transform.position + debugDirection * 2f, 0.2f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, protectionRadius);

            if (targetEnemy != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(targetEnemy.position, 0.3f);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(debugShieldPosition, 0.3f);
        }


        public bool IsPointOnLineSegment(Vector3 A, Vector3 B, Vector3 P, float tolerance = 0.5f)
        {
            // Wektor AB i AP
            Vector3 AB = B - A;
            Vector3 AP = P - A;

            // Obliczenie wartoœci t dla rzutowania punktu P na prost¹ AB
            float t = Vector3.Dot(AP, AB) / Vector3.Dot(AB, AB);

            // Obliczenie najbli¿szego punktu na linii
            Vector3 closestPoint = A + Mathf.Clamp01(t) * AB;

            // Sprawdzenie odleg³oœci P od najbli¿szego punktu na linii
            float distance = Vector3.Distance(P, closestPoint);

            // Zwracamy true, jeœli odleg³oœæ jest mniejsza lub równa tolerancji
            return distance <= tolerance;
        }


        // Function to calculate the closest point on the line segment from point P
        public static Vector3 ClosestPointOnLineSegment(Vector3 A, Vector3 B, Vector3 P)
        {
            // Calculate the vector AB and normalize it
            Vector3 AB = B - A;
            Vector3 AP = P - A;

            // Calculate the projection of vector AP onto AB
            float t = Vector3.Dot(AP, AB) / Vector3.Dot(AB, AB);

            // Clamp t to be between 0 and 1 to ensure the closest point is within the segment
            t = Mathf.Clamp01(t);

            // Calculate the closest point on the line (using the parametric equation)
            Vector3 closestPoint = A + t * AB;

            return closestPoint;
        }


    }
}