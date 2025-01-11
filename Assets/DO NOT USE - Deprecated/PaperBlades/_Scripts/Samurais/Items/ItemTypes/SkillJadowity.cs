using UnityEngine;

[CreateAssetMenu(menuName = "Character/Skill/Jadowity")]
public class SkillJadowity : ItemSO
{
    public int dmgPerTick;
    public int ticks;
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
            if (!UnitInRange(origin, enemy, Range))
                continue;

            StatusManager statusManager = enemy.GetComponent<StatusManager>();
            statusManager.ApplyPoison(dmgPerTick, ticks);
        }
        Cooldown.ResetTimers();
    }
}
