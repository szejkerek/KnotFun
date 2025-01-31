using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera mainCamera; // Kamera u¿ywana do obliczeñ, przypisz j¹ w Inspectorze

    void Update()
    {
        // Pobierz pozycjê myszy na ekranie
        Vector3 mousePosition = Input.mousePosition;

        // Rzutuj pozycjê myszy na œwiat 3D
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // Pobierz punkt, w który celuje mysz
            Vector3 targetPosition = hitInfo.point;

            // Oblicz kierunek od postaci do celu
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0; // Zignoruj komponent osi Y, aby ograniczyæ obrót do osi Y

            // Jeœli kierunek nie jest zerowy, obróæ postaæ
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }
}
