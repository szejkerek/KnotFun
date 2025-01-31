using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera mainCamera; // Kamera u�ywana do oblicze�, przypisz j� w Inspectorze

    void Update()
    {
        // Pobierz pozycj� myszy na ekranie
        Vector3 mousePosition = Input.mousePosition;

        // Rzutuj pozycj� myszy na �wiat 3D
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // Pobierz punkt, w kt�ry celuje mysz
            Vector3 targetPosition = hitInfo.point;

            // Oblicz kierunek od postaci do celu
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0; // Zignoruj komponent osi Y, aby ograniczy� obr�t do osi Y

            // Je�li kierunek nie jest zerowy, obr�� posta�
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }
}
