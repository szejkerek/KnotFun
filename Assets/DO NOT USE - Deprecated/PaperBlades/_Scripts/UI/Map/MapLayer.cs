using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapLayer
{

    public List<MapNodeTypeProbability> mapNodeTypesProbability;

    public MapNodeType DrawRandomNodeType()
    {
        float totalProbability = 0;
        foreach (var nodeTypeProbability in mapNodeTypesProbability)
        {
            totalProbability += nodeTypeProbability.probability;
        }

        float randomValue = Random.Range(0f, totalProbability);
        float cumulativeProbability = 0f;

        foreach (var nodeTypeProbability in mapNodeTypesProbability)
        {
            cumulativeProbability += nodeTypeProbability.probability;
            if (randomValue <= cumulativeProbability)
            {
                return nodeTypeProbability.mapNodeType;
            }
        }

        return MapNodeType.Arena;
    }

}

[System.Serializable]
public class MapNodeTypeProbability
{
    public MapNodeType mapNodeType;
    [Range(0f, 1f)]
    public float probability;
}