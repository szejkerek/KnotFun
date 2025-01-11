using UnityEngine;

[CreateAssetMenu(fileName = "NewSword", menuName = "Character/Items/Sword", order = 1)]
public class SwordItemSO : ItemSO
{
    [field: Header("Sword")]

    [field: SerializeField] public GameObject HitParticle { get; private set; }
    [field: SerializeField] public AnimationClip swordAnimation { get; private set; }
    [field: SerializeField] public DamageDisplay damageDisplay { get; private set; }

    public override void Use(IUnit target, IUnit origin)
    {
        if (!Cooldown.IsOffCooldown())
            return;

        if (!UnitInRange(target, origin, Range))
            return;
        int dmg = origin.CalculateDamage();
        SpawnParticle(origin.AttackPoint.position);
        target.TakeDamage(dmg);
        SpawnDamageDisplay(dmg, target, origin);
        Cooldown.ResetTimers();

    }

    private void SpawnParticle(Vector3 position)
    {
        var particle = Instantiate(HitParticle, position, Quaternion.identity);
        Destroy(particle, 0.3f);
    }

    private void SpawnDamageDisplay(int dmg, IUnit target, IUnit origin)
    {
        var display = Instantiate(damageDisplay);
        display.Init(dmg, origin.AttackPoint.transform.position, target.AttackPoint.transform.position);
        //Destroy(display.gameObject, 3f);
    }
}
