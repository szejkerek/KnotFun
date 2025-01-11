using System;
using UnityEngine;
using UnityEngine.AI;

public interface IActionStrategy
{
    bool CanPerform { get; }
    bool Complete { get; }

    void Start()
    {
        //noop
    }

    void Update(float deltaTime)
    {
        //noop
    }

    void Stop()
    {
        //noop
    }
}

public class LoadAttackStrategy : IActionStrategy
{
    public bool CanPerform => true;
    public bool Complete { get; private set; }

    readonly CountdownTimer timer;

    public LoadAttackStrategy(float duration)
    {
        timer = new CountdownTimer(duration);
        timer.OnTimerStart += () => Complete = false;
        timer.OnTimerStop += () => Complete = true;
    }

    public void Start() => timer.Start();
    public void Update(float deltaTime) => timer.Tick(deltaTime);
}

public class AttackStrategy : IActionStrategy
{
    public bool CanPerform => true; // Agent can always attack
    public bool Complete { get; private set; }

    public Animator animator;

    public Sensor sensor;
    public GoapAgent agent;

    readonly CountdownTimer timer;
    //readonly AnimationController animations;

    //public AttackStrategy(AnimationController animations)
    public AttackStrategy(float duration, Sensor attackSensor, int attackDamage, Animator animator, GoapAgent agent)
    {
        sensor = attackSensor;
        this.animator = animator;
        this.agent = agent;

        //  this.animations = animations;
        //timer = new CountdownTimer(animations.GetAnimationLength(animations.attackClip));
        timer = new CountdownTimer(duration);
        timer.OnTimerStart += () => Complete = false;
        timer.OnTimerStop += () => Complete = true;
        timer.OnTimerStop += () => DealDamage();
    }

    public void Start()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", true);
        Transform T = animator.gameObject.transform.parent.transform;
        T.LookAt(sensor.target.transform);


        //T.rotation = Quaternion.Euler(0, T.rotation.y, 0);
        timer.Start();
    }


    //public void Update(float deltaTime) => timer.Tick(deltaTime);
    public void Update(float deltaTime)
    {
        timer.Tick(deltaTime);
        //UnityEngine.Debug.LogError($"ATTACK!!! Progress: {Math.Round(timer.Progress * 100, 2)}% of {Mathf.Pow(timer.Progress / timer.Time, -1)}s");
        //UnityEngine.Debug.LogError($"ATTACK!!! Attacking the Player for {Mathf.Pow(timer.Progress / timer.Time, -1)}s");
    }

    public void DealDamage()
    {
        if (sensor == null) return;
        if (sensor.target == null) return;
        if (sensor.target.TryGetComponent(out IUnit unit))
        {
            agent.GetComponent<IUnit>().AttackTarget(unit);
        }
        animator.SetBool("isAttacking", false);
    }


}

public class MoveStrategy : IActionStrategy
{
    readonly NavMeshAgent agent;
    readonly Func<Vector3> destination;
    readonly Animator animator;

    public bool CanPerform => !Complete;
    public bool Complete => agent.remainingDistance <= 3f && !agent.pathPending;

    public MoveStrategy(NavMeshAgent agent, Func<Vector3> destination, Animator animator)
    {
        this.agent = agent;
        this.destination = destination;
        this.animator = animator;
    }

    public void Start()
    {
        animator.SetBool("isMoving", true);
        animator.SetBool("isAttacking", false);
        agent.SetDestination(destination());
    }
    public void Stop()
    {
        agent.ResetPath();
        animator.SetBool("isMoving", false);
    }
}

public class WanderStrategy : IActionStrategy
{
    readonly NavMeshAgent agent;
    readonly float wanderRadius;
    readonly Animator animator;

    public bool CanPerform => !Complete;
    public bool Complete => agent.remainingDistance <= 2f && !agent.pathPending;

    public WanderStrategy(NavMeshAgent agent, float wanderRadius, Animator animator)
    {
        this.agent = agent;
        this.wanderRadius = wanderRadius;
        this.animator = animator;
    }

    public void Start()
    {
        animator.SetBool("isMoving", true);
        animator.SetBool("isAttacking", false);
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomDirection = (UnityEngine.Random.insideUnitSphere * wanderRadius).With(y: 0);
            NavMeshHit hit;

            if (NavMesh.SamplePosition(agent.transform.position + randomDirection, out hit, wanderRadius, 1))
            {
                agent.SetDestination(hit.position);
                return;
            }
        }
    }
}

public class IdleStrategy : IActionStrategy
{
    public bool CanPerform => true; // Agent can always Idle, nothing is gointt to stop us from doing that 
    public bool Complete { get; private set; }

    private Animator animator;

    readonly CountdownTimer timer;

    public IdleStrategy(float duration, Animator animator)
    {
        timer = new CountdownTimer(duration);
        timer.OnTimerStart += () => Complete = false;
        timer.OnTimerStop += () => Complete = true;

        this.animator = animator;
    }

    public void Start()
    {
        timer.Start();

        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", false);
    }

    public void Update(float deltaTime) => timer.Tick(deltaTime);
}
