using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Passsives/ShamanDebuffAOE")]
public class PassiveShamanDebuffAOE : PassiveEffectSO
{
    public float damageDebuff;
    public float range;
    public List<Enemy> affectedEnemies = new List<Enemy>();

    public override string GetDesctiption()
    {
        return $"lessens the attack of wereinks by {damageDebuff * 100:0} %";
    }

    public override string GetName()
    {
        return "Shaman";
    }

    public override void OnStart(SamuraiEffectsManager context)
    {
        affectedEnemies.Clear();
    }

    public override void OnUpdate(SamuraiEffectsManager context, float deltaTime)
    {

        List<Enemy> stillAffectedEnemies = new List<Enemy>();

        foreach (Enemy enemy in affectedEnemies)
        {
            if (enemy != null)
            {
                if (Vector3.Distance(context.transform.position, enemy.transform.position) > range)
                {
                    enemy.GetComponent<StatusManager>().RevertWeakness();
                }
                else
                {
                    stillAffectedEnemies.Add(enemy);
                }
            }
        }

        affectedEnemies = stillAffectedEnemies;
        foreach (Enemy enemy in context.Enemies)
        {
            if (enemy == null)
                continue;

            if (Vector3.Distance(context.transform.position, enemy.transform.position) <= range)
            {
                if (!affectedEnemies.Contains(enemy))
                {
                    affectedEnemies.Add(enemy);
                    enemy.GetComponent<StatusManager>().ApplyWeakness(damageDebuff);
                }
            }
        }
    }
}
