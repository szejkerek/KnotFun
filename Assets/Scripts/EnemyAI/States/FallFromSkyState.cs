using UnityEngine;

public class FallFromSkyState : EnemyState
{
    Vector3 spawnPosition;
    private float fallSpeed = 10f; // Speed of falling
    private bool isFalling = true; // Flag to check if it's falling
    private float timeOnGround = 0f; // Timer for how long the enemy has been on the ground

    public FallFromSkyState(EnemyStateMachine stateMachine, Vector3 spawnPosition) : base(stateMachine)
    {
        this.spawnPosition = spawnPosition;
    }

    public override void Enter()
    {
        // Set the enemy's starting position (falling from spawnPosition, but at a height of 20)
        stateMachine.gameObject.transform.position = spawnPosition.With(y: 30);
        isFalling = true;
        timeOnGround = 0f;
    }

    public override void Update()
    {
        if (isFalling)
        {
            // Simulate falling by adjusting the Y position based on gravity
            Vector3 currentPosition = stateMachine.gameObject.transform.position;

            // Only change the Y position (falling vertically)
            currentPosition.y -= fallSpeed * Time.deltaTime;

            // Check if a sphere of radius 0.75 is touching the ground
            // We check below the enemy's current position, adjusting by 0.75 for the sphere radius
            Vector3 sphereCenter = new Vector3(currentPosition.x, currentPosition.y - 0.75f, currentPosition.z);

            // Check if the sphere is in the "Ground" layer
            if (Physics.CheckSphere(sphereCenter, 0.75f, stateMachine.GroundLayer))
            {
                currentPosition.y = 0f; // Set the Y position to the ground level
                isFalling = false; // Stop falling
                timeOnGround = 1f; // Start counting time on the ground
            }

            // Apply the new position
            stateMachine.gameObject.transform.position = currentPosition;
        }
        else
        {
            // When the enemy hits the ground, count time and then switch to ChaseState
            timeOnGround -= Time.deltaTime;

            if (timeOnGround <= 0f)
            {
                // Transition to ChaseState after 1 second on the ground
                stateMachine.ChangeState(new ChaseState(stateMachine));
            }
        }
    }


    public override void Exit()
    {
        Debug.Log("Exiting FallFromSkyState");
    }
}
