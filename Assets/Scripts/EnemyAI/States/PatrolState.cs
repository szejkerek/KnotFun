using UnityEngine;

public class PatrolState : EnemyState
{
    public PatrolState(EnemyStateMachineBase stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public override void Update()
    {
        Debug.Log("Patrolling...");
        // Example transition to AttackState
        if (Vector3.Distance(Vector3.zero, stateMachine.transform.position) < 2f) // Dummy condition
        {
            stateMachine.ChangeState(new AttackState(stateMachine));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Patrol State");
    }
}