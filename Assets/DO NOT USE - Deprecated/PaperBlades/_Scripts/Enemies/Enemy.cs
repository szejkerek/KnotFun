using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IUnit
{
    public CharacterStats temporaryStats;

    public AssetReferenceItemSO Weapon;
    private Item weaponItem;


    public static Action<Enemy> OnEnemyKilled;
    public Action<Enemy> OnDamaged;

    private EnemyGenerator generator;
    public bool IsAlly => false;
    public CharacterStats CharacterStats;
    public int moneyOnKill = 50;

    public Transform AttackPoint => attackPoint;

    public Action<IUnit> OnAttack { get; set; }

    [SerializeField] Transform attackPoint;

    public int Health { get; private set; }

    public ParticleSystem particles;


    private void Awake()
    {
        generator = GetComponent<EnemyGenerator>();
        GetComponent<NavMeshAgent>().speed = CharacterStats.Speed;
        weaponItem = new Item(Weapon.AssetGUID);
        Health = GetStats().MaxHealth;
        GetComponentInChildren<HealthBarEnemy>().Init(this);
    }

    public void TakeDamage(int valueHP)
    {
        int damageReducedByArmor = Mathf.Max(1, valueHP - CharacterStats.Armor);
        Health -= damageReducedByArmor;
        InfoTextManager.Instance.AddInformation($"Inky took {damageReducedByArmor} damage.", InfoLenght.Short);

        if (Health < 0)
        {
            OnEnemyKilled?.Invoke(this);
            Destroy(gameObject);
        }
        else
        {
            generator.GetDamage((float)Health / (float)GetStats().MaxHealth);
        }
        OnDamaged?.Invoke(this);
    }
    public void HealUnit(int valueHP)
    {
        Health = Mathf.Min(Health + valueHP, GetStats().MaxHealth);
    }

    public void HealToMax()
    {
        Health = GetStats().MaxHealth;
    }

    public CharacterStats GetStats()
    {
        return CharacterStats + weaponItem.GetStats() + temporaryStats;
    }

    public int GetHealth()
    {
        return Health;
    }

    public void AttackTarget(IUnit target)
    {
        OnAttack?.Invoke(target);
        weaponItem.itemData.Use(target, this);
    }

    private void OnDestroy()
    {
        if (particles != null)
        {
            particles.emissionRate = 0;
            Destroy(particles.GetComponent<EnemyParticle>(), 5);
            Destroy(particles, 15);
        }
    }
}
