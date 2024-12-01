using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackState : EnemyState
{
    float timer;
    private Player target;
    public AttackState(EnemyStateMachine stateMachine, Player target) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        Shoot();
    }

    private void Shoot()
    {
        Debug.Log("Shoot!");
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 3)
            stateMachine.ChangeState(new ChaseState(stateMachine));
    }

    public override void Exit()
    {

    }
}