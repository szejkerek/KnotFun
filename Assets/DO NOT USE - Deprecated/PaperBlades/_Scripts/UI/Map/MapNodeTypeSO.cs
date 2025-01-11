using UnityEngine;

public enum MapNodeType
{
    Arena,
    WeaponReroll,
    Forge,
    Armory,
    Experience,
    Temple,
    Boss
}


[CreateAssetMenu(fileName = "NewMapNodeType", menuName = "Map/MapNodeType")]
public class MapNodeTypeSO : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { private set; get; }
    [field: SerializeField] public Sprite IslandImage { private set; get; }
    [field: SerializeField] public MapNodeType MapNodeType { private set; get; }
}
