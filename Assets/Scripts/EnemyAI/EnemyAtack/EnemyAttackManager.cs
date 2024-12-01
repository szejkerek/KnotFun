using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    private Enemy _enemy;
    
    List<Player> _players;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _players = FindObjectsByType<Player>(FindObjectsSortMode.None).ToList();
    }

    public Player GetClosestPlayer()
    {
        _players = _players.OrderBy(player => Vector3.Distance(transform.position, player.transform.position)).ToList();
        Player targetPlayer = null;
        if (_players.Count > 1)
        {
            int randomChoice = UnityEngine.Random.Range(0, 2);
            targetPlayer = randomChoice == 0 ? _players[0] : _players[1];
        }
        else
        {
            targetPlayer = _players[0];
        }

        return targetPlayer;
    }
}
