using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 moveDirection;
    private float speed;          

    public void Initialize(Vector3 direction, float speed)
    {
        this.moveDirection = direction;
        this.speed = speed;
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}