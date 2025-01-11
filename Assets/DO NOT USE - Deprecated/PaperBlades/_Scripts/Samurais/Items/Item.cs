
[System.Serializable]
public class Item : IDisplayable
{
    public string DisplayIconGuid => IconGuid;
    public string DisplayName => Name;

    public string IconGuid;
    public string SpriteGuid;
    public string Name;
    public int MaxLevel;
    public int Level;
    public int Cost;
    public string itemDataGuid;

    public ItemSO itemData;

    public Item(string itemDataGuid)
    {
        this.itemDataGuid = itemDataGuid;
        itemData = new AssetReferenceItemSO(itemDataGuid).LoadAssetAsync<ItemSO>().WaitForCompletion();
        this.IconGuid = itemData.Icon.AssetGUID;
        this.SpriteGuid = itemData.GameSprite.AssetGUID;
        this.Name = itemData.Name;
        this.MaxLevel = itemData.MaxLevel;
        this.Cost = itemData.Cost;
    }

    public int CostFunction()
    {
        return Cost * (Level + 1);
    }

    public CharacterStats GetStats()
    {
        return itemData.CalculateLevelAdditions(this.Level);
    }

    public Item TryGetNextItem()
    {
        if (string.IsNullOrEmpty(itemData.NextItem.AssetGUID))
        {
            return null;
        }

        return new Item(itemData.NextItem.AssetGUID);
    }
}
