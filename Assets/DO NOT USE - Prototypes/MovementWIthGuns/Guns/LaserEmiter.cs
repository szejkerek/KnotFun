using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolders
{
    public class LaserEmiter : MonoBehaviour, IGun
    {
        public ParticleSystem bulletParticleSystem;

        bool isShooting = false;

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            bulletParticleSystem = GetComponent<ParticleSystem>();
        }

        private void OnParticleCollision(GameObject other)
        {
            int events = bulletParticleSystem.GetCollisionEvents(other, collisionEvents);
            if (other.tag == "Enemy")
            {
                Debug.Log("Hit");
            }
        }

        public void Use()
        {
            bulletParticleSystem.Play();
        }
    }
}
