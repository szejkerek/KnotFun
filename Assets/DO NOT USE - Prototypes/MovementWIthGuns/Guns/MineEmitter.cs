using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolders
{
    public class MineEmiter : MonoBehaviour, IGun
    {
        public ParticleSystem bulletParticleSystem;
        public ParticleSystem mineParticleSystem;
        public LayerMask collisionLayerMask;
        public float sphereRadius;

        public Color gizmoColor = Color.red; // Kolor gizmo
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            bulletParticleSystem = GetComponent<ParticleSystem>();

        }

        private void OnParticleCollision(GameObject other)
        {
            int events = bulletParticleSystem.GetCollisionEvents(other, collisionEvents);

            float timeToExplode = mineParticleSystem.main.startLifetime.constant;
            StartCoroutine(WaitForExplosion(timeToExplode));
        }

        IEnumerator WaitForExplosion(float waitTime)
        {
            // Czekaj przez okre�lony czas
            yield return new WaitForSeconds(waitTime);

            // Po odczekaniu wykonaj jak�� akcj�
            Debug.Log("Czas min��!");

            Collider[] colliders = Physics.OverlapSphere(collisionEvents[0].intersection, sphereRadius, collisionLayerMask);

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
            if (collisionEvents.Count == 0) return;
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(collisionEvents[0].intersection, sphereRadius);
        }

        public void Use()
        {
            bulletParticleSystem.Play();
        }
    }
}
