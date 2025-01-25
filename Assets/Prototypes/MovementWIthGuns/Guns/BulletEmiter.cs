using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolders
{
    public class BulletEmiter : MonoBehaviour
    {
        public ParticleSystem bulletParticleSystem;

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
            if (other.tag == "Enemy")
            {
                Debug.Log("Hit");
            }
        }

    }
}
