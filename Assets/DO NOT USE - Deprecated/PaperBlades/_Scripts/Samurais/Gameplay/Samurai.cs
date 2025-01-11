using System;
using UnityEngine;

public abstract class Samurai : MonoBehaviour, IUnit
{
    public CharacterStats temporaryStats;
    public bool IsAlly => true;
    public Character Character { get; private set; }

    public Transform AttackPoint => attackPoint;
    public Action<IUnit> OnAttack { get; set; }

    [SerializeField] Transform attackPoint;

    SamuraiStylizer samuraiStylizer;
    SamuraiRenderers samuraiRenderer;
    SamuraiEffectsManager samuraiEffectsManager;

    protected abstract void OnSamuraiDeath();

    public void SetCharacterData(Character character)
    {
        this.Character = character;
    }

    protected virtual void Start()
    {
        samuraiStylizer = GetComponent<SamuraiStylizer>();
        samuraiRenderer = GetComponentInChildren<SamuraiRenderers>();
        samuraiEffectsManager = GetComponent<SamuraiEffectsManager>();
        Character.SamuraiVisuals.Apply(samuraiStylizer.Renderers, Character);
        samuraiEffectsManager.Initialize(Character);
    }
    public void UseSkill()
    {
        Character.Skill.itemData.Use(null, this);
    }

    public void TakeDamage(int valueHP)
    {
        CharacterStats stats = GetStats();
        int damageReducedByArmor = Mathf.Max(1, valueHP - stats.Armor);
        Character.LostHealth += damageReducedByArmor;

        if (stats.MaxHealth <= Character.LostHealth)
        {
            OnSamuraiDeath();
        }

        InfoTextManager.Instance.AddInformation($"{Character.DisplayName} took {damageReducedByArmor} damage.", InfoLenght.Short);

        samuraiRenderer.SetDamagePercent((float)Character.LostHealth / (float)stats.MaxHealth);

        Character.OnHealthChange?.Invoke();

    }

    public void HealUnit(int valueHP)
    {
        Character.LostHealth -= valueHP;
        if (Character.LostHealth < 0)
        {
            Character.LostHealth = 0;
            Debug.Log("I'm full healed");
        }
        else
        {
            Debug.Log($"{name} was healed for {valueHP}");
        }
        Character.OnHealthChange?.Invoke();
    }

    public void HealToMax()
    {
        Character.LostHealth = 0;
        Character.OnHealthChange?.Invoke();
    }

    public CharacterStats GetStats()
    {
        return Character.GetStats() + temporaryStats;
    }

    public int GetHealth()
    {
        return GetStats().MaxHealth - Character.LostHealth;
    }

    public void AttackTarget(IUnit target)
    {
        OnAttack?.Invoke(target);
        Character.Weapon.itemData.Use(target, this);
    }
}
