using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTeam", menuName = "Character/Team", order = 0)]
public class TeamSO : ScriptableObject
{

    [field: SerializeField] public AssetReferenceCharacterSO General { private set; get; }
    [field: SerializeField] public List<AssetReferenceCharacterSO> TeamMembers { private set; get; }

}
