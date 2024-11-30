using UnityEngine;

// Abstract base class for states
public abstract class EnemyState
{
    protected EnemyStateMachineBase stateMachine;

    public EnemyState(EnemyStateMachineBase stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { } 
    public virtual void Update() { } 
    public virtual void Exit() { }
}

public class EnemyStateMachineBase : MonoBehaviour
{
    private EnemyState currentState;
    public void Init(EnemyState initialState)
    {
        currentState = initialState;
        currentState.Enter();
    }
    private void Update()
    {
        if (currentState != null)
            currentState.Update();
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;

        if (currentState != null)
            currentState.Enter();
    }
}

// Patrol state

// Attack state