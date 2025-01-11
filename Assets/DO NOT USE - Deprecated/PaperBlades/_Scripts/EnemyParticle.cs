using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EnemyParticle : MonoBehaviour
{
    [SerializeField] Transform ObjectToFollow;
    [SerializeField] Enemy enemy;

    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        enemy.particles = particle;
        transform.parent = null;
        particle.Play();
    }

    private void Update()
    {
        if (ObjectToFollow != null) transform.position = ObjectToFollow.position;
    }
}

