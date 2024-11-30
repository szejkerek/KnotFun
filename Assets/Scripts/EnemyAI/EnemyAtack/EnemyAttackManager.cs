using System;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    private Enemy _enemy;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }
}
