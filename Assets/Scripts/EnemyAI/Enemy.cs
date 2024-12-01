using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyConfig enemyConfig;
    public EnemyStateMachine stateMachine;

    public Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        stateMachine.enemy = this;
        stateMachine.ChangeState(new FallFromSkyState(stateMachine));
    }

    public void SetAnimationVariable(bool value, string name)
    {
        if(animator != null)
        animator.SetBool(name, value);
    }
}