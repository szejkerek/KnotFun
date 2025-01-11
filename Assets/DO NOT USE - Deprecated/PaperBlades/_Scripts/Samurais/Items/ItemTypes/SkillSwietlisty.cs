using UnityEngine;

[CreateAssetMenu(menuName = "Character/Skill/Swietlisty")]
public class SkillSwietlisty : ItemSO
{
    public int damage;
    public override void Use(IUnit target, IUnit origin)
    {
        SamuraiEffectsManager manager = origin.gameObject.GetComponent<SamuraiEffectsManager>();
        if (manager == null)
            return;

        if (!Cooldown.IsOffCooldown())
            return;
        manager.Roar();
        foreach (Enemy enemy in manager.Enemies)
        {
            if (enemy == null) continue;
            if (!UnitInRange(origin, enemy, Range))
                continue;

            enemy.TakeDamage(origin.GetStats().Damage + damage);
        }
        Cooldown.ResetTimers();
    }
}
