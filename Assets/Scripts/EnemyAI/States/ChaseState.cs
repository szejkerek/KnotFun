using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyState
{
    private NavMeshAgent _navMeshAgent;
    private Player _target;

    public ChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _navMeshAgent = stateMachine.NavMeshAgent;
        _target = stateMachine.EnemyAttackManager.GetClosestPlayer();
    }

    public override void Enter()
    {
        Debug.Log("Entering Chase State");

        if (_navMeshAgent != null && _target != null)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_target.transform.position);
        }
    }

    public override void Update()
    {
        if (_navMeshAgent == null || _target == null)
            return;
        
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            stateMachine.ChangeState(new AttackState(stateMachine));
        }
        else
        {
            _navMeshAgent.SetDestination(_target.transform.position);
            Debug.Log("Chasing target...");
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Chase State");

        if (_navMeshAgent != null)
        {
            _navMeshAgent.isStopped = true;
        }
    }
}