using UnityEngine;

public class EnemyWeaponBase : MonoBehaviour
{
    private EnemyConfig _config;
    public Transform attackPoint;

    public void Init(EnemyConfig config)
    {
        _config = config;
    }
    public bool CanUseWeapon(GameObject target)
    {
        return true;
    }

    public void UseWeapon()
    {
        Debug.Log("Weapon used!");
    }
}
