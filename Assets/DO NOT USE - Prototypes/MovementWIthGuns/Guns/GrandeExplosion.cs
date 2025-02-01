using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolders
{
    public class GrandeExplosion : MonoBehaviour
    {
        public ParticleSystem granadeExplosionParticleSystem;
        private void Start()
        {
            granadeExplosionParticleSystem = GetComponent<ParticleSystem>();
        }

        private void OnParticleTrigger()
        {
            // particles
            List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
            ParticleSystem.ColliderData colliderData;

            // get
            int numEnter = granadeExplosionParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside, out colliderData);
            Debug.Log("TriggetedParticles: " + numEnter);
            for (int i = 0; i < numEnter; i++) 
            { 
                int colliderCount = colliderData.GetColliderCount(i);
                Debug.Log("CollideserCount: " + colliderCount);
                for (int j = 0; j < colliderCount; j++) 
                {
                    var collider = colliderData.GetCollider(i, j);
                    if (collider.tag == "Enemy")
                    {
                        Debug.Log("Hit" + collider.name);
                    }
                }
            }
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created

    }
}
