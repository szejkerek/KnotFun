using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player player1;
    public List<ConnectionType> connectionType12 = new List<ConnectionType>();
    public Player player2;
    public List<ConnectionType> connectionType23 = new List<ConnectionType>();
    public Player player3;
    public List<ConnectionType> connectionType31 = new List<ConnectionType>();

    public float speed = 5f; // Speed of player movement
    public float pullSpeed = 2f; // Speed of enforcing maximum distance

    void Update()
    {
        HandlePlayerMovement(player1, KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
        HandlePlayerMovement(player2, KeyCode.T, KeyCode.G, KeyCode.F, KeyCode.H);
        HandlePlayerMovement(player3, KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L);

        // Enforce distances with conditions
        EnforceMaxDistance(player1, player2, connectionType12.Last());
        EnforceMaxDistance(player2, player3, connectionType23.Last());
        EnforceMaxDistance(player3, player1, connectionType31.Last());
    }

    void HandlePlayerMovement(Player player, KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(up)) moveDirection += Vector3.forward;
        if (Input.GetKey(down)) moveDirection += Vector3.back;
        if (Input.GetKey(left)) moveDirection += Vector3.left;
        if (Input.GetKey(right)) moveDirection += Vector3.right;

        player.transform.position += moveDirection * speed * Time.deltaTime;
    }

    void EnforceMaxDistance(Player playerA, Player playerB, ConnectionType connection)
    {
        float maxDistance = connection != ConnectionType.Loose ? 5f : 10f;
        float distance = Vector3.Distance(playerA.transform.position, playerB.transform.position);

        if (distance > maxDistance)
        {
            Vector3 direction = (playerA.transform.position - playerB.transform.position).normalized;
            float excessDistance = distance - maxDistance;
            
            Vector3 adjustment = direction * excessDistance * 0.5f; 
            playerA.transform.position -= adjustment * pullSpeed * Time.deltaTime;
            playerB.transform.position += adjustment * pullSpeed * Time.deltaTime;
        }
    }
}


public enum ConnectionType
{
    God,
    Red,
    Green,
    Blue,
    Loose
}
