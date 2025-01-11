using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SamuraiEffectsManager : MonoBehaviour
{
    List<IPassiveEffect> passives = new List<IPassiveEffect>();

    public Samurai Owner;
    public List<Samurai> Team;
    public List<Enemy> Enemies;
    public ParticleSystem particleSystemRoar;

    public void Initialize(Character character)
    {
        GatherPassives(character);
        GatherMapUnits();
        Owner = GetComponent<Samurai>();

        passives.ForEach(e => e.OnStart(this));
        particleSystemRoar.Stop();
    }

    private void OnEnable()
    {
        EnemySpawner.OnEnemySpawned += () => GatherMapUnits();
        Enemy.OnEnemyKilled += (enemy) => GatherMapUnits();
        SamuraiAlly.OnDeath += (samurai) => GatherMapUnits();
    }

    private void GatherMapUnits()
    {

        Team = FindObjectsOfType<Samurai>().ToList();
        Team.Remove(Owner);

        Enemies = FindObjectsOfType<Enemy>().ToList();
    }

    private void OnDisable()
    {
        EnemySpawner.OnEnemySpawned -= () => GatherMapUnits();
        Enemy.OnEnemyKilled -= (enemy) => GatherMapUnits();
        SamuraiAlly.OnDeath -= (samurai) => GatherMapUnits();
    }

    private void GatherPassives(Character character)
    {
        character.PassiveEffects.ForEach(e => passives.Add(e));
        GetComponent<IUnit>().OnAttack += (target) => { passives.ForEach(e => e.OnAttack(this, target)); };
    }

    private void Update()
    {
        passives.ForEach(e => e.OnUpdate(this, Time.deltaTime));
    }

    public void Roar()
    {
        particleSystemRoar.Play();
        StartCoroutine(RoarParticlesCall(1f));
    }

    IEnumerator RoarParticlesCall(float timer)
    {
        yield return new WaitForSeconds(timer);
        particleSystemRoar.Stop();
    }
}
