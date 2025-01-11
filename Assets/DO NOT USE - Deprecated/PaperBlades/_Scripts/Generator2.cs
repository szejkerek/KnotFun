using System.Collections.Generic;
//using Unity.AI.Navigation;
using UnityEngine;

public class Generator2 : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 spawnRotation;
    public List<GameObject> liGoSpawn = new List<GameObject>();
    public GameObject floor;

    public void Spawn()
    {
        if (floor == null)
        {
            Debug.LogError("Floor object is not assigned!");
            return;
        }

        Collider floorCollider = floor.GetComponent<Collider>();
        if (floorCollider == null)
        {
            Debug.LogError("Floor object does not have a Collider!");
            return;
        }

        Vector3 floorSize = floorCollider.bounds.size;
        Debug.Log(floorSize);


        float randomX = Random.Range(-floorSize.x / 2, floorSize.x / 2);
        float randomZ = Random.Range(-floorSize.z / 2, floorSize.z / 2);


        GameObject goToSpawn = liGoSpawn[Random.Range(0, liGoSpawn.Count)];
        //Collider objCollider = goToSpawn.GetComponent<Collider>();

        Vector3 spawnPosition = new Vector3(randomX, 0, randomZ) + transform.position + offset;

        Quaternion rotation = Quaternion.Euler(spawnRotation);

        GameObject SpawnedObject = Instantiate(goToSpawn, spawnPosition, rotation);

        float high = SpawnedObject.GetComponent<Collider>().bounds.size.y;
        float newhight = 0;
        newhight += high / 2 + floor.transform.position.y + floorSize.y / 2;

        SpawnedObject.transform.position = new Vector3(SpawnedObject.transform.position.x, newhight, SpawnedObject.transform.position.z);
    }

}