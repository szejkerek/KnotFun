using System;
using UnityEngine;

public class RopeLoop : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "EnemyBullet")
            return;
        
        Destroy(other.gameObject);
        
    }
}
