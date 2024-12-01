using UnityEngine;

public abstract class EnemyWeaponBase : MonoBehaviour
{
    private EnemyConfig _config;
    public Transform attackPoint;

    public void Init(EnemyConfig config)
    {
        _config = config;
    }
    public abstract void UseWeapon(Transform target);
}