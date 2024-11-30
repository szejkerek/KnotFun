using UnityEngine;

public class StunnedState : EnemyState
{
    public StunnedState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Stunned State");
    }

    public override void Update()
    {
        Debug.Log("Stunned...");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Stunned State");
    }
}