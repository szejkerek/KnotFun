using UnityEngine;

public class PatrolState : EnemyState
{
    public PatrolState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public override void Update()
    {
        Debug.Log("Patrolling...");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Patrol State");
    }
}