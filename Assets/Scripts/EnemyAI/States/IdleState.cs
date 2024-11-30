using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void Update()
    {
        Debug.Log("Idle State: Waiting...");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}