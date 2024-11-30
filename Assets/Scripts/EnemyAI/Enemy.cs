using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isStationary = false;
    public EnemyConfig enemyConfig;
    public EnemyStateMachine stateMachine;
    
    private void Start()
    {
        stateMachine.ChangeState(new AttackState(stateMachine, isStationary));
    }
}