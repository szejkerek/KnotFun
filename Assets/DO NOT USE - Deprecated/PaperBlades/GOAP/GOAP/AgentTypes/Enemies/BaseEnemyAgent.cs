public class BaseEnemyAgent : GoapAgent
{


    protected override void SetupBeliefs()
    {
        base.SetupBeliefs();
    }

    protected override void SetupActions()
    {
        base.SetupActions();

        actions.Add(new AgentAction.Builder("AttackEnemy")
            .WithStrategy(new AttackStrategy(0.67f, attackSensor, 10, animator, this))
            .AddPrecondition(beliefs["EnemyInAttackRange"])
            .AddEffect(beliefs["AttackingEnemy"])
            .Build());
    }

    protected override void SetupGoals()
    {
        base.SetupGoals();


    }
}
