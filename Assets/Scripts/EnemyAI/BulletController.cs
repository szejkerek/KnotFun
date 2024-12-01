using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject ropeHitViusal;
    public GameObject bloodHitVisual;

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
        if(collision.gameObject.CompareTag("Enemy"))
            return;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.collider.TryGetComponent(out RopeLoop loop))
            {
                loop.ChargeRope(0.2f);
                var visual = Instantiate(ropeHitViusal, transform.position, Quaternion.identity);
                Destroy(visual.gameObject, 3f);
            }

            if (collision.collider.TryGetComponent(out Player player))
            {
                player.KillPlayer();
                var blood = Instantiate(bloodHitVisual, transform.position, Quaternion.identity);
                Destroy(blood.gameObject, 3f);
            }
            
            Destroy(gameObject);
        }
        
        
    }
}