[System.Serializable]
public class GOAPConfig
{
    public AttackStyle AttackStyle;
    public PriorityTarget PriorityTarget;
    public IdleBehaviour IdleBehaviour;
    public Mobilne Mobilne;
}

[System.Serializable]
public enum AttackStyle
{
    Aggresive,
    Normal,
    Defensive
}

[System.Serializable]
public enum PriorityTarget
{
    StrongEnemy,
    WeakEnemy,
    Any
}

[System.Serializable]
public enum IdleBehaviour
{
    Patrol,
    CoverPosition
}

[System.Serializable]
public enum Mobilne
{
    ToMe,
    Spread,
    BackOff
}