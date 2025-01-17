using UnityEngine;

public class NewMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float frequency = 1f; // Frequency of the sinusoidal motion
    [SerializeField] private float offset = 0f;   // Offset for the sinusoidal motion
    [SerializeField] private float power = 50f;   // Offset for the sinusoidal motion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Calculate forward and backward movement using sine wave
        float forwardMovement = Mathf.Sin(Time.time * frequency + offset);

        // Set movement direction to sinusoidal forward/backward
        Vector3 movementDir = new Vector3(0, 0, forwardMovement);

        // Apply force to the rigidbody
        rb.AddForce(movementDir.normalized * Time.deltaTime * speed * power);
    }
}