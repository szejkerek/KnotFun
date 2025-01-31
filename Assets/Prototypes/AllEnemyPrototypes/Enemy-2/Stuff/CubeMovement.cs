using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Movement speed of the cube

    void Update()
    {
        // Get input from the keyboard (WASD)
        float horizontal = Input.GetAxis("Horizontal"); // A (-1) and D (+1)
        float vertical = Input.GetAxis("Vertical");     // W (+1) and S (-1)

        // Create movement direction based on input
        Vector3 movement = new Vector3(vertical, 0, horizontal);

        // Apply movement to the cube
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
