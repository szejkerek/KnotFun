using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    
    private EnemyState currentState;
    public NavMeshAgent NavMeshAgent {get; private set;}
    public LayerMask GroundLayer;
    public EnemyAttackManager EnemyAttackManager {get; private set;}

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();    
        EnemyAttackManager = GetComponent<EnemyAttackManager>();    
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
        Debug.Log($"State changed to {nameof(newState)}");

        if (currentState != null)
            currentState.Enter();
    }
}