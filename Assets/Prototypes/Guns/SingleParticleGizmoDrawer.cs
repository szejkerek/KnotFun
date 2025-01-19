using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleGizmoDrawer : MonoBehaviour
{
    public float sphereRadius = 0.5f; // Rozmiar sfery
    public Color gizmoColor = Color.red; // Kolor gizmo
    public LayerMask collisionLayerMask;

    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;


    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        
    }
    private void Update()
    {
        if (particleSystem == null || particles == null)
            return;

        int numParticlesAlive = particleSystem.GetParticles(particles);
        Debug.Log("ParticleCount: " + numParticlesAlive);
        // Iteruj przez cz¹steczki i rysuj sfery w ich pozycjach
        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 particlePosition = particles[i].position;

            Collider[] colliders = Physics.OverlapSphere(particlePosition, sphereRadius, collisionLayerMask);

            // Wypisz znalezione kolidery w konsoli (tylko w edytorze)
            foreach (Collider collider in colliders)
            {
                Debug.Log($"Znaleziono obiekt: {collider.gameObject.name}");
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (particleSystem == null || particles == null)
            return;

        // Pobierz aktualne cz¹steczki
        int numParticlesAlive = particleSystem.GetParticles(particles);

        Gizmos.color = gizmoColor;

        Debug.Log("PArticleCount: " + numParticlesAlive);
        // Iteruj przez cz¹steczki i rysuj sfery w ich pozycjach
        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 particlePosition = particles[i].position;

            Gizmos.DrawSphere(particlePosition, sphereRadius);

            Collider[] colliders = Physics.OverlapSphere(particlePosition, sphereRadius, collisionLayerMask);

            // Wypisz znalezione kolidery w konsoli (tylko w edytorze)
            foreach (Collider collider in colliders)
            {
                Debug.Log($"Znaleziono obiekt: {collider.gameObject.name}");
            }
        }
    }
}
