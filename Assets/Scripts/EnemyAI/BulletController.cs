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
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.collider.TryGetComponent(out RopeLoop loop))
            {
                loop.ChargeRope(0.2f);
            }

            if (collision.collider.TryGetComponent(out Player player))
            {
                player.KillPlayer();
            }
        }
        Destroy(gameObject);
    }
}