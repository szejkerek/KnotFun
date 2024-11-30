using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(EnemyStateMachineBase stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Attack State");
    }

    public override void Update()
    {
        Debug.Log("Attacking...");
        if (Time.timeSinceLevelLoad > 10f)
        {
            stateMachine.ChangeState(new IdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
}