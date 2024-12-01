using UnityEngine;

public class FallFromSkyState : EnemyState
{
    private float fallSpeed = 10f; // Speed of falling
    private bool isFalling = true; // Flag to check if it's falling
    private float timeOnGround = 0f; // Timer for how long the enemy has been on the ground

    public FallFromSkyState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.enemy.SetAnimationVariable(true, "IsJump");
        isFalling = true;
        timeOnGround = 0f;
    }

    public override void Update()
    {
        if (isFalling)
        {

            if (Physics.CheckSphere(stateMachine.transform.position.Add(y: -0.75f), 0.75f, stateMachine.GroundLayer))
            {
                isFalling = false; // Stop falling
                timeOnGround = 2f; // Start counting time on the ground
            }
        }
        else
        {
            timeOnGround -= Time.deltaTime;

            if (timeOnGround <= 0f)
            {
                stateMachine.ChangeState(new ChaseState(stateMachine));
            }
        }
    }


    public override void Exit()
    {
        stateMachine.enemy.SetAnimationVariable(false, "IsJump");
        Debug.Log("Exiting FallFromSkyState");
    }
}
