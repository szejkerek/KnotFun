using UnityEngine;

public abstract class PassiveEffectSO : ScriptableObject, IPassiveEffect
{
    public abstract string GetName();
    public abstract string GetDesctiption();
    public virtual void OnStart(SamuraiEffectsManager context) { }
    public virtual void OnAttack(SamuraiEffectsManager context, IUnit target) { }
    public virtual void OnUpdate(SamuraiEffectsManager context, float deltaTime) { }

}
