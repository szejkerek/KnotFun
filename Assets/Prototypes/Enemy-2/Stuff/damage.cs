using UnityEngine;

namespace PlaceHolders
{
    public class Damage : MonoBehaviour
    {
        [SerializeField] private string BlueTag; // Assign your prefab's tag in the Inspector or set it here.
        [SerializeField] private string RedTag; // Assign your prefab's tag in the Inspector or set it here.

        private float blueCollisionTime = -1f; // Tracks the last time a collision with BlueTag occurred
        private float redCollisionTime = -1f; // Tracks the last time a collision with RedTag occurred
        private float collisionDelay = 1f; // Time window to detect both collisions

        void OnCollisionEnter(Collision collision)
        {
            // Check if the colliding object has the BlueTag
            if (collision.gameObject.CompareTag(BlueTag))
            {
                blueCollisionTime = Time.time; // Update the time of the last BlueTag collision
                Debug.Log("BlueHit");

                // Check if RedTag collision happened recently
                if (Time.time - redCollisionTime <= collisionDelay)
                {
                    Debug.Log("ROZPIERDOL");
                }
            }
            // Check if the colliding object has the RedTag
            else if (collision.gameObject.CompareTag(RedTag))
            {
                redCollisionTime = Time.time; // Update the time of the last RedTag collision
                Debug.Log("RedHit");

                // Check if BlueTag collision happened recently
                if (Time.time - blueCollisionTime <= collisionDelay)
                {
                    Debug.Log("ROZPIERDOL");
                }
            }
        }
    }
}
