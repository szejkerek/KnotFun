using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/Character", order = 0)]
public class CharacterSO : ScriptableObject
{
    [field: SerializeField] public SamuraiNames Name { private set; get; }
    [field: SerializeField] public SamuraiVisualsSO SamuraiVisuals { private set; get; }
    [field: SerializeField] public GameObject CharacterGameObject { private set; get; }
    [field: SerializeField] public CharacterStats BaseStats { private set; get; }
    [field: SerializeField] public List<PassiveEffectSO> PassiveEffects { private set; get; }
    [field: SerializeField] public AssetReferenceItemSO Weapon { private set; get; }
    [field: SerializeField] public AssetReferenceItemSO Armor { private set; get; }
    [field: SerializeField] public AssetReferenceItemSO Skill { private set; get; }
    //[field: SerializeField] public GOAPConfig GOAPConfig { private set; get; }
}
