using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject boom;
    
    
    public float health = 100;

    public void DecreaseHealth(float amount)
    {
        health -= amount;
        if (health < 0) Kill();
    }

    void Kill()
    {
        var boom = Instantiate(this.boom, transform.position, Quaternion.identity);
        Destroy(boom.gameObject, 5f);
        Destroy(gameObject);
    }

}
