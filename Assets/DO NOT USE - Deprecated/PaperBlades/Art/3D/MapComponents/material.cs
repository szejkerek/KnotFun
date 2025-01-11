using UnityEngine;

public class SetTerrainMaterial : MonoBehaviour
{
    public Material material;

    void Start()
    {
        GetComponent<Terrain>().materialTemplate = material; ;
    }
}