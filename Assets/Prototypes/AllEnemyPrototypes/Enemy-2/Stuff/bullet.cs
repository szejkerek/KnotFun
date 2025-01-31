using UnityEngine;

namespace PlaceHolders
{
    public class bullet : MonoBehaviour
    {
        public float life = 3;

        void Awake()
        {
            Destroy(gameObject, life);
        }

        void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
