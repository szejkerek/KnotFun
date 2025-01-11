using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class AttackState : EnemyState
{
    float timer;
    private NavMeshAgent _navMeshAgent;
    private Player target;
    public AttackState(EnemyStateMachine stateMachine, Player target) : base(stateMachine)
    {
        _navMeshAgent = stateMachine.NavMeshAgent;
        this.target = target;
    }

    public override void Enter()
    {
        stateMachine.enemy.SetAnimationVariable(true, "IsShooting");
        Shoot();
    }

    private void Shoot()
    {
        stateMachine.EnemyAttackManager.Attack(target);
    }

    public override void Update()
    {
        _navMeshAgent.SetDestination(stateMachine.gameObject.transform.position);
        timer += Time.deltaTime;
        if(timer >= 3)
            stateMachine.ChangeState(new ChaseState(stateMachine));
        _navMeshAgent.transform.LookAt(target.transform.position);
    }

    public override void Exit()
    {
        stateMachine.enemy.SetAnimationVariable(false, "IsShooting");
    }
}