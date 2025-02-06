using UnityEngine;

namespace PlaceHolders
{
    public class DuoSphereController : MonoBehaviour
    {
        public Transform sphereA;
        public Transform sphereB;
        public float rotationSpeed = 100f;    // Pr�dko�� obrotu
        public float orbitSpeed = 20f;        // Pr�dko�� ruchu po okr�gu
        public float orbitRadius = 5f;
        public LineRenderer laser;            // ��cznik laserowy

        private float orbitAngle = 0f;
        private Vector3 orbitCenter;

        void Start()
        {
            if (laser != null)
            {
                laser.positionCount = 2;
                laser.startWidth = 0.05f; // Cie�szy laser na pocz�tku
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
            // Obliczenie �rodka mi�dzy sferami
            Vector3 center = (sphereA.position + sphereB.position) / 2;

            // Obracanie sfer wok� �rodka w p�aszczy�nie XZ (o� Y)
            sphereA.RotateAround(center, Vector3.up, rotationSpeed * Time.deltaTime);
            sphereB.RotateAround(center, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        void OrbitAroundCenter()
        {
            // Aktualizacja k�ta orbity
            orbitAngle += orbitSpeed * Time.deltaTime;

            // Obliczanie nowej pozycji na okr�gu
            float x = orbitCenter.x + Mathf.Cos(orbitAngle * Mathf.Deg2Rad) * orbitRadius;
            float z = orbitCenter.z + Mathf.Sin(orbitAngle * Mathf.Deg2Rad) * orbitRadius;

            // Ustawienie nowej pozycji obiektu
            transform.position = new Vector3(x, transform.position.y, z);
        }
    }
}
