
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class EnemyChance
{
    public GameObject EnemyPrefab;
    [Range(0f, 1f)] public float chanceToSpawn;
}

public class EnemySpawner : MonoBehaviour
{
    public Interval<float> spawnInterval;
    public static Action OnEnemySpawned;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<EnemyChance> enemyPrefabs;

    int spawnedEnemies;
    Samurai samurai;

    public void Init(int maxEnemies, int atOnceLimit)
    {
        StartCoroutine(SpawnEnemyRoutine(maxEnemies, atOnceLimit));
        samurai = FindObjectOfType<SamuraiGeneral>();
    }

    private IEnumerator SpawnEnemyRoutine(int maxEnemies, int atOnceLimit)
    {
        while (true)
        {
            float waitTime = UnityEngine.Random.Range(spawnInterval.BottomBound, spawnInterval.UpperBound);
            yield return new WaitForSeconds(waitTime);

            int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[spawnIndex];
            if (ActiveEnemyCount() < atOnceLimit)
            {
                var enemy = Instantiate(PickEnemy(), spawnPoint.position, spawnPoint.rotation);
                enemy.transform.LookAt(samurai.transform);
                OnEnemySpawned?.Invoke();
                spawnedEnemies++;
                if (spawnedEnemies >= maxEnemies)
                {
                    break;
                }

            }
        }
    }

    int ActiveEnemyCount()
    {
        return FindObjectsOfType<Enemy>().ToList().Count();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color of the Gizmos
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint != null)
            {
                Gizmos.DrawSphere(spawnPoint.position, 1f);
            }
        }
    }

    GameObject PickEnemy()
    {
        List<EnemyChance> enemies = new();
        float r = UnityEngine.Random.Range(0f, 1f);
        float sum = 0;
        foreach (var item in enemyPrefabs)
        {
            sum += item.chanceToSpawn;
            if (sum >= r) return item.EnemyPrefab;
            //if (UnityEngine.Random.Range(0f,1f) <= item.chanceToSpawn)
            //{
            //    enemies.Add(item);
            //}
        }
        return enemyPrefabs.Last().EnemyPrefab;
        //return enemies.SelectRandomElement().EnemyPrefab;
    }
}
