
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSamuraiVisuals", menuName = "Character/SamuraiVisuals")]
public class SamuraiVisualsSO : ScriptableObject
{
    [field: SerializeField] public List<Sprite> Helmet_B { private set; get; }
    [field: SerializeField] public List<Sprite> Helmet_F { private set; get; }
    [field: SerializeField] public List<Sprite> Face_F { private set; get; }
    [field: SerializeField] public List<Sprite> Head { private set; get; }
    [field: SerializeField] public List<Sprite> Pants { private set; get; }
}
