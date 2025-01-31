using UnityEngine;
using System.Collections;

public class TrainSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab to spawn
    public Transform spawnPoint; // The location to spawn the object
    public float spawnInterval = 1f; // Time interval for spawning

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnObject()
    {
        if (objectToSpawn != null && spawnPoint != null)
        {
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Missing objectToSpawn or spawnPoint!");
        }
    }
}