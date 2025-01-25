using UnityEngine;

namespace PlaceHolders
{
    public class gun : MonoBehaviour
    {
        public Transform bulletSpawnPoint;
        public Transform bulletSpawnPoint2;
        public GameObject bulletPrefab;
        public float bulletSpeed = 10;

        void Update()
        {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
           var bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint2.position, bulletSpawnPoint2.rotation);
           bullet.GetComponent<Rigidbody>().linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
           bullet2.GetComponent<Rigidbody>().linearVelocity = bulletSpawnPoint2.forward * bulletSpeed;
        }
        }
    }
}
