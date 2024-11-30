using System.Collections.Generic;
using System.Linq;
using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    public Player player1;
    public Rope rope12;
    public List<ConnectionType> connectionType12 = new List<ConnectionType>();
    public Player player2;
    public Rope rope23;
    public List<ConnectionType> connectionType23 = new List<ConnectionType>();
    public Player player3;
    public Rope rope31;
    public List<ConnectionType> connectionType31 = new List<ConnectionType>();
    
    [Header("Player config")]
    public float speed = 5f;
    [Header("Rope config")]
    public float pullSpeed = 2f; 
    public float ropeLenghtTangled = 5f;
    public float ropeLenghtLoose = 10f;
    public float springStrength = 5f;

    public Vector3 GetMiddlePoint()
    {
        Vector3 position1 = player1.transform.position;
        Vector3 position2 = player2.transform.position;
        Vector3 position3 = player3.transform.position;

        return (position1 + position2 + position3) / 3;
    }

    public float GetAvgDistance()
    {
        Vector3 position1 = player1.transform.position;
        Vector3 position2 = player2.transform.position;
        Vector3 position3 = player3.transform.position;

        float distance12 = Vector3.Distance(position1, position2);
        float distance23 = Vector3.Distance(position2, position3);
        float distance31 = Vector3.Distance(position3, position1);

        return (distance12 + distance23 + distance31) / 3;
    }

    
    void Update()
    {
        HandlePlayerMovement(player1, KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
        HandlePlayerMovement(player2, KeyCode.T, KeyCode.G, KeyCode.F, KeyCode.H);
        HandlePlayerMovement(player3, KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L);
        
        EnforceMaxDistance(player1, player2, connectionType12.Last());
        EnforceMaxDistance(player2, player3, connectionType23.Last());
        EnforceMaxDistance(player3, player1, connectionType31.Last());
        
        player1.Move();
        player2.Move();
        player3.Move();
    }

    void HandlePlayerMovement(Player player, KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        player.currentDirection = player.GetMovementDirection(up,down,left,right).normalized * speed * Time.deltaTime;
    }

    void EnforceMaxDistance(Player playerA, Player playerB, ConnectionType connection)
    {
        float maxDistance = connection switch
        {
            ConnectionType.Loose => ropeLenghtLoose,
            ConnectionType.None => float.MaxValue,
            _ => ropeLenghtTangled
        };

        float distance = Vector3.Distance(playerA.transform.position, playerB.transform.position);

        if (distance > maxDistance)
        {
            Vector3 direction = (playerA.transform.position - playerB.transform.position).normalized;
            float excessDistance = distance - maxDistance;

            Vector3 springForce = direction * (excessDistance * springStrength) * Time.deltaTime;

            playerA.currentDirection -= springForce;
            playerB.currentDirection += springForce;
            
            Vector3 midpoint = (playerA.transform.position + playerB.transform.position) / 2;
            Vector3 clampedOffset = direction * (maxDistance / 2);

            playerA.transform.position = midpoint + clampedOffset;
            playerB.transform.position = midpoint - clampedOffset;
        }
    }


}