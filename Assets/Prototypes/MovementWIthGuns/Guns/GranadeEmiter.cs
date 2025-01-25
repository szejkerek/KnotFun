using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace PlaceHolders
{
    public class GranadeEmiter : MonoBehaviour
    {
        public ParticleSystem bulletParticleSystem;
        public LayerMask collisionLayerMask;
        public float sphereRadius;

        public Color gizmoColor = Color.red; // Kolor gizmo
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            bulletParticleSystem = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                bulletParticleSystem.Play();
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            int events = bulletParticleSystem.GetCollisionEvents(other, collisionEvents);

            

            Collider[] colliders = Physics.OverlapSphere(collisionEvents[0].intersection, sphereRadius, collisionLayerMask);

            // Wypisz znalezione kolidery w konsoli (tylko w edytorze)
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    Debug.Log($"Znaleziono obiekt: {collider.gameObject.name}");
                }
            }

            
        }

        private void OnDrawGizmos()
        {
            if(collisionEvents.Count == 0) return;
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(collisionEvents[0].intersection, sphereRadius);
        }
    }
}
