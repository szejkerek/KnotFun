using UnityEngine;

namespace PlaceHolders
{
    public class Train : MonoBehaviour
    {
        public float speed = 5f; // Speed of movement
        public string destinationTag = "Destination"; // Tag for destination

        private Transform locationB;
        private float despawnTime = 1f; // Time before despawn
        private float timer;

        void Start()
        {
            timer = 0f;
            GameObject destinationObject = GameObject.FindGameObjectWithTag(destinationTag);
            if (destinationObject != null)
            {
                locationB = destinationObject.transform;
            }
            else
            {
                Debug.LogWarning("Destination with tag " + destinationTag + " not found!");
            }
        }

        void Update()
        {
            if (locationB == null) return;
            
            // Move forward
            transform.position += transform.forward * speed * Time.deltaTime;
            
            // Check if time exceeded or reached an object with the destination tag
            timer += Time.deltaTime;
            GameObject[] destinations = GameObject.FindGameObjectsWithTag(destinationTag);
            foreach (GameObject dest in destinations)
            {
                if (Vector3.Distance(transform.position, dest.transform.position) <= 0.5f)
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }
}
