
using UnityEngine.AddressableAssets;

[System.Serializable]
public class AssetReferenceItemSO : AssetReferenceT<ItemSO>
{
    public AssetReferenceItemSO(string guid) : base(guid) { }
}

[System.Serializable]
public class AssetReferenceCharacterSO : AssetReferenceT<CharacterSO>
{
    public AssetReferenceCharacterSO(string guid) : base(guid) { }
}
[System.Serializable]
public class AssetReferenceSamuraiVisuals : AssetReferenceT<SamuraiVisualsSO>
{
    public AssetReferenceSamuraiVisuals(string guid) : base(guid) { }
}

[System.Serializable]
public class AssetReferencePassiveEffect : AssetReferenceT<PassiveEffectSO>
{
    public AssetReferencePassiveEffect(string guid) : base(guid) { }
}