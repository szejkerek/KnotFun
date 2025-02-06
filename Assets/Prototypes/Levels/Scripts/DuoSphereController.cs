using UnityEngine;

namespace PlaceHolders
{
    public class DuoSphereController : MonoBehaviour
    {
        public Transform sphereA;
        public Transform sphereB;
        public float rotationSpeed = 100f;    // Prêdkoœæ obrotu
        public float orbitSpeed = 20f;        // Prêdkoœæ ruchu po okrêgu
        public float orbitRadius = 5f;
        public LineRenderer laser;            // £¹cznik laserowy

        private float orbitAngle = 0f;
        private Vector3 orbitCenter;

        void Start()
        {
            if (laser != null)
            {
                laser.positionCount = 2;
                laser.startWidth = 0.05f; // Cieñszy laser na pocz¹tku
                laser.endWidth = 0.05f;
            }
            orbitCenter = transform.position;
        }

        void Update()
        {
            RotateSpheres();
            OrbitAroundCenter();

            // Aktualizacja pozycji lasera
            if (laser != null)
            {
                laser.SetPosition(0, sphereA.position);
                laser.SetPosition(1, sphereB.position);
            }
        }

        void RotateSpheres()
        {
            // Obliczenie œrodka miêdzy sferami
            Vector3 center = (sphereA.position + sphereB.position) / 2;

            // Obracanie sfer wokó³ œrodka w p³aszczyŸnie XZ (oœ Y)
            sphereA.RotateAround(center, Vector3.up, rotationSpeed * Time.deltaTime);
            sphereB.RotateAround(center, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        void OrbitAroundCenter()
        {
            // Aktualizacja k¹ta orbity
            orbitAngle += orbitSpeed * Time.deltaTime;

            // Obliczanie nowej pozycji na okrêgu
            float x = orbitCenter.x + Mathf.Cos(orbitAngle * Mathf.Deg2Rad) * orbitRadius;
            float z = orbitCenter.z + Mathf.Sin(orbitAngle * Mathf.Deg2Rad) * orbitRadius;

            // Ustawienie nowej pozycji obiektu
            transform.position = new Vector3(x, transform.position.y, z);
        }
    }
}
