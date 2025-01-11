public interface IPassiveEffect
{
    public string GetName();
    public string GetDesctiption();
    public void OnStart(SamuraiEffectsManager context);
    public void OnAttack(SamuraiEffectsManager context, IUnit target);
    public void OnUpdate(SamuraiEffectsManager context, float deltaTime);
}
