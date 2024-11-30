using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyConfig enemyConfig;
    EnemyStateMachineBase stateMachine;
    

    private void Start()
    {
        stateMachine.Init(new IdleState(stateMachine));
    }

    private void Update()
    {
        // State machine will automatically update
    }
}