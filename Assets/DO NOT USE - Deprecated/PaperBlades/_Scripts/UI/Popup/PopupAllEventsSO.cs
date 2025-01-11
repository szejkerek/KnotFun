using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PopupAllEventsSO : ScriptableObject
{
    public List<PopupEventSO> ForgeEvents;
    public List<PopupEventSO> ExperienceEvents;
    public List<PopupEventSO> TempleEvents;
    public List<PopupEventSO> BossEvents;
}
