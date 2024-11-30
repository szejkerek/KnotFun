using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState currentState;
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
        Debug.Log($"State changed to {nameof(newState)}");

        if (currentState != null)
            currentState.Enter();
    }
}