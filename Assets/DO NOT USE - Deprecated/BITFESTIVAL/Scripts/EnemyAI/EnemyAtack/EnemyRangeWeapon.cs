using UnityEngine;

public class EnemyRangeWeapon : EnemyWeaponBase
{
    public BulletController bulletPrefab;
    public float bulletSpeed = 10f;

    public override void UseWeapon(Transform target)
    {
        BulletController bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        Vector3 direction = (target.position - attackPoint.position).normalized;
        bullet.Initialize(direction, bulletSpeed);
    }
}