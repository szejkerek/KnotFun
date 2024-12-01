using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Entering Attack State");
    }

    public override void Update()
    {
        Debug.Log($"Attacking ...");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
}