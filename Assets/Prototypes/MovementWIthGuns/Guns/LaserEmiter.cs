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

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (!isShooting)
                {
                    bulletParticleSystem.Play();
                    isShooting = true;
                }
            }
            else
            {
                bulletParticleSystem.Stop();
                isShooting = false;
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

        public void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
