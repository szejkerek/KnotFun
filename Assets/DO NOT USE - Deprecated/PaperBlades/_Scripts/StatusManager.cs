using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StatusManager : MonoBehaviour
{
    bool isWeakenessApplied = false;
    int weaknessValue = 0;
    [SerializeField] GameObject weaknessParticle;

    bool isPoisonApplied = false;
    float timeToTick = 0.5f;
    [SerializeField] GameObject poisonParticle;

    bool isStunApplied = false;

    public void ApplyStun(float timer)
    {
        StartCoroutine(ApplyStunCorutine(timer));
    }
    IEnumerator ApplyStunCorutine(float timer)
    {
        Debug.Log("Stunned");
        isStunApplied = true;

        //GoapAgent agent = GetComponent<GoapAgent>();
        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = !isStunApplied;
        //agent.enabled = !isStunApplied;
        //agent.GetComponentInChildren<Animator>().enabled = !isStunApplied;
        yield return new WaitForSeconds(timer);

        isStunApplied = false;
        navAgent.enabled = !isStunApplied;
        // agent.enabled = !isStunApplied;
        //agent.GetComponentInChildren<Animator>().enabled = !isStunApplied;
    }
    public void RevertStun()
    {
        isStunApplied = false;
        //GoapAgent agent = GetComponent<GoapAgent>();
        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = !isStunApplied;
        //agent.enabled = !isStunApplied;
        // agent.GetComponentInChildren<Animator>().enabled = !isStunApplied;
    }
    public void ApplyWeakness(float timer, float weaknessRatio)
    {
        StartCoroutine(ApplyWeaknessCorutine(timer, weaknessRatio));
    }
    IEnumerator ApplyWeaknessCorutine(float timer, float weaknessRatio)
    {
        ApplyWeakness(weaknessRatio);
        yield return new WaitForSeconds(timer);
        RevertWeakness();
    }
    public void ApplyWeakness(float weaknessRatio)
    {
        isWeakenessApplied = true;
        weaknessParticle.SetActive(isWeakenessApplied);
        IUnit unit = GetComponent<IUnit>();
        weaknessValue = (int)((float)unit.GetStats().Damage * weaknessRatio);
        WeaknessEffect(isWeakenessApplied);
    }
    public void RevertWeakness()
    {
        if (isWeakenessApplied)
        {
            isWeakenessApplied = false;
            weaknessParticle.SetActive(isWeakenessApplied);
            WeaknessEffect(isWeakenessApplied);
        }
        weaknessValue = 0;
    }

    public void ApplyPoison(float dmgPerTick, int ticks)
    {
        StartCoroutine(ApplyPoisonCorutine(dmgPerTick, ticks));
    }
    IEnumerator ApplyPoisonCorutine(float dmgPerTick, int ticks)
    {
        isPoisonApplied = true;
        poisonParticle.SetActive(isPoisonApplied);
        int appliedTicks = 0;
        Debug.Log("Poisoned");
        IUnit unit = GetComponent<IUnit>();
        while (appliedTicks < ticks)
        {
            if (isPoisonApplied) unit.TakeDamage((int)dmgPerTick);
            yield return new WaitForSeconds(timeToTick);
            appliedTicks++;
        }
        isPoisonApplied = false;
        poisonParticle.SetActive(isPoisonApplied);
    }

    public void RevertPoison()
    {
        isPoisonApplied = false;
        poisonParticle.SetActive(isPoisonApplied);
    }

    public void ApplyCleanse()
    {
        RevertStun();
        RevertWeakness();
        RevertPoison();
        StopAllCoroutines();
    }

    public void WeaknessEffect(bool apply)
    {
        CharacterStats stats = new CharacterStats();
        stats.Damage = weaknessValue;
        stats.Damage *= ((apply) ? -1 : 1);

        if (TryGetComponent<Samurai>(out Samurai samurai))
        {
            samurai.temporaryStats += stats;
        }
        if (TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.temporaryStats += stats;
        }

    }
}
