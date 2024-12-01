using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> sceneEnemies = new List<Enemy>();

    public BoxCollider spawnArea; // The area where enemies will spawn
    public Enemy enemyPrefab; // The enemy prefab to spawn
    public int maxEnemies = 6; // Max number of enemies in the scene
    private float spawnInterval = 3f; // Interval to spawn new enemies
    private float amountIncreaseOverTime = 10;

    public int startEnmies = 3;
    
    
    
    private void Start()
    {
        for (int i = 0; i <startEnmies; i++)
        {
            SpawnEnemy();
        }
        
        StartCoroutine(SpawnEnemies());
        StartCoroutine(IncreaseAmount());
        CentralCrystal.onWin += KillAllEnemies;
    }

    private void KillAllEnemies()
    {
        foreach (var enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList())
        {
            Destroy(enemy.gameObject);
        }
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Wait for the next spawn interval
            yield return new WaitForSeconds(spawnInterval);
            if (FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList().Count < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private IEnumerator IncreaseAmount()
    {
        while (true)
        {
            yield return new WaitForSeconds(amountIncreaseOverTime);

            maxEnemies += 1;
        }
    }

    private void SpawnEnemy()
    {
        // Get random position within the spawn area
        Vector3 spawnPosition = GetRandomPositionInCollider(spawnArea);

        // Instantiate the enemy and add it to the list
        Enemy newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        sceneEnemies.Add(newEnemy);
    }

    private Vector3 GetRandomPositionInCollider(BoxCollider collider)
    {
        // Get random position within the bounds of the collider
        Vector3 center = collider.bounds.center;
        Vector3 size = collider.bounds.size;

        // Random point within the BoxCollider bounds
        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(randomX, randomY, randomZ);
    }
}
