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

    private void EnsureAgentOnNavMesh()
    {
        if (!_navMeshAgent.isOnNavMesh)
        {
            Vector3 position = _navMeshAgent.transform.position;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas))
            {
                _navMeshAgent.Warp(hit.position);
                Debug.Log("Repositioned NavMeshAgent onto the NavMesh.");
            }
            else
            {
                Debug.LogError("Failed to find a valid NavMesh position.");
            }
        }
    }

    public override void Enter()
    {
        stateMachine.enemy.SetAnimationVariable(true, "IsMoving");

        if (_navMeshAgent != null && _target != null)
        {
            EnsureAgentOnNavMesh();
            _navMeshAgent.SetDestination(_target.transform.position);
            _navMeshAgent.isStopped = false;
        }
    }

    public override void Update()
    {
        if (_navMeshAgent == null || _target == null)
            return;
        
        if(!_navMeshAgent.isOnNavMesh)
            return;
        
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            stateMachine.ChangeState(new AttackState(stateMachine, _target));
        }
        else
        {
            _navMeshAgent.SetDestination(_target.transform.position);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Chase State");
        stateMachine.enemy.SetAnimationVariable(false, "IsMoving");
        if (_navMeshAgent != null)
        {
            _navMeshAgent.isStopped = true;
        }
    }
}