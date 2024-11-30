using UnityEngine;

public class EnemyRangeWeapon : EnemyWeaponBase
{
    public override bool CanUseWeapon(GameObject target)
    {
        return true;
    }

    public override void UseWeapon()
    {
        Debug.Log("Weapon used");
    }
}