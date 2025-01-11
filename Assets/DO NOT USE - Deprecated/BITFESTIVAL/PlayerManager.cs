using System;
using System.Collections.Generic;
using System.Linq;
using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    public Player player1;
    public RopeManager rope12;
    
    public Player player2;
    public RopeManager rope23;
    
    public Player player3;
    public RopeManager rope31;
    
    [Header("Player config")]
    public float speed = 5f;
    [Header("Rope config")]
    public float pullSpeed = 2f; 
    public float ropeLenghtTangled = 5f;
    public float ropeLenghtLoose = 10f;
    public float springStrength = 5f;

    private void Awake()
    {

    }

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
        HandlePlayerMovement(player1);
        HandlePlayerMovement(player2);
        HandlePlayerMovement(player3);
        
        EnforceMaxDistance(player1, player2, rope12);
        EnforceMaxDistance(player2, player3, rope23);
        EnforceMaxDistance(player3, player1, rope31);
        
        player1.Move();
        player2.Move();
        player3.Move();
    }

    void HandlePlayerMovement(Player player)
    {
        player.currentDirection = player.GetMovementDirection().normalized * speed * Time.deltaTime;
    }

    void EnforceMaxDistance(Player playerA, Player playerB, RopeManager rope)
    {
        // Constants
        float maxDistance = rope.currentPullDistance;
        float gravityStrength = 9.81f;  // Gravity strength in m/s^2, can adjust if needed

        // Apply gravity first (affects y-axis)
        Vector3 gravityForce = Vector3.up * -gravityStrength * Time.deltaTime;

        // Apply gravity to players' current direction
        playerA.currentDirection += gravityForce;
        playerB.currentDirection += gravityForce;
    
        // Optionally, update player positions based on current direction and gravity
        playerA.transform.position += playerA.currentDirection * Time.deltaTime;
        playerB.transform.position += playerB.currentDirection * Time.deltaTime;

        // Calculate the current distance between the players
        float distance = Vector3.Distance(playerA.transform.position, playerB.transform.position);

        if (distance > maxDistance)
        {
            // Calculate the direction from player B to player A
            Vector3 direction = (playerA.transform.position - playerB.transform.position).normalized;
            float excessDistance = distance - maxDistance;

            // Spring force to restore max distance
            Vector3 springForce = direction * (excessDistance * springStrength) * Time.deltaTime;

            // Apply spring force to players' current direction
            playerA.currentDirection -= springForce;
            playerB.currentDirection += springForce;
        
            // Calculate the midpoint of the two players
            Vector3 midpoint = (playerA.transform.position + playerB.transform.position) / 2;
            Vector3 clampedOffset = direction * (maxDistance / 2);

            // Move players to keep them within the max distance
            playerA.transform.position = midpoint + clampedOffset;
            playerB.transform.position = midpoint - clampedOffset;
        }
    }



}