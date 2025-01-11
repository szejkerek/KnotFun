using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class ItemSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public AssetReferenceSprite Icon { get; private set; }
    [field: SerializeField] public AssetReferenceSprite GameSprite { get; private set; }
    [field: SerializeField] public int MaxLevel { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    public Cooldown Cooldown = new Cooldown();
    [field: SerializeField] public AssetReferenceItemSO NextItem { get; private set; }
    [field: SerializeField] public CharacterStats BaseStats { get; private set; }
    [field: SerializeField] public CharacterStats StatsModifiersPerLevel { get; private set; }
    [field: SerializeField] public float Range { get; private set; }

    public CharacterStats CalculateLevelAdditions(int level)
    {
        return BaseStats + (StatsModifiersPerLevel * level);
    }

    public abstract void Use(IUnit target, IUnit origin);

    public static bool UnitInRange(IUnit target, IUnit origin, float range)
    {
        if (range <= 0)
        {
            Debug.Log($"Range set to infinity. Unit is in range");
            return true;
        }
        return Vector3.Distance(target.gameObject.transform.position, origin.gameObject.transform.position) <= range;
    }

}
