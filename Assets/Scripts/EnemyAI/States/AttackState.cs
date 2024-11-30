using UnityEngine;

public class AttackState : EnemyState
{
    private bool isStationary;

    public AttackState(EnemyStateMachine stateMachine, bool isStationary) : base(stateMachine)
    {
        this.isStationary = isStationary;
    }

    public override void Enter()
    {
        Debug.Log("Entering Attack State");
    }

    public override void Update()
    {
        Debug.Log($"Attacking {isStationary}...");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
}