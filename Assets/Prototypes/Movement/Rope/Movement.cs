using UnityEngine;

public class NewMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 movementDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.AddForce(movementDir.normalized * Time.deltaTime * speed);
    }
}
