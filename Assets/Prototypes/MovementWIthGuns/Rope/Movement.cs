using UnityEngine;

public class NewMovement : MonoBehaviour
{
    public float speed = 10f; // Speed multiplier for movement
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get input from WASD keys or Arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveVertical = Input.GetAxis("Vertical");     // W/S or Up/Down

        // Calculate the direction based on input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply force to the Rigidbody
        rb.AddForce(movement * speed * Time.deltaTime);
    }
}