using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(EnemyStateMachineBase stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void Update()
    {
        Debug.Log("Idle State: Waiting...");
        // Example transition to PatrolState
        if (Time.timeSinceLevelLoad > 5f) // Dummy condition
        {
            stateMachine.ChangeState(new PatrolState(stateMachine));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}